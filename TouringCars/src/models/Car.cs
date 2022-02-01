namespace TouringCars
{
    public class Car
    {
        public String owner;
        public Automerken brand;
        public Route route;

        private int kmDriven;
        private int fuel;
        private int maxFuel;
        private bool locked;

        public Car(String owner)
        {
            this.owner = owner;
            this.kmDriven = 0;
            this.maxFuel = FixedParams.maxCarFuel;
            this.fuel = FixedParams.startingFuel;
            this.locked = true;
            this.brand = (Automerken)new Random().Next(0, Enum.GetNames(typeof(Automerken)).Length);
            this.route = new Route();
        }

        public Car(String owner, Automerken brand) : this(owner)
        {
            this.brand = brand;
        }

        public int getKMDriven()
        {
            return this.kmDriven;
        }

        public int getFuel()
        {
            return this.fuel;
        }

        public void emptyTank()
        {
            this.fuel = 0;
        }

        public String go(Boolean showOverride = false)
        {
            String result = "";
            result += $"{this.owner} is starting the tour..\n";
            var lockCheck = checkLock();
            result += lockCheck.Item1;
            if (!lockCheck.Item2)
            {
                while (!route.hasFinished)
                {
                    var next = route.getNextPoint();
                    int distanceToNext = next.Item2;
                    while (distanceToNext > 0 && !this.route.hasFinished)
                    {
                        var res = this.drive();
                        distanceToNext -= res.Item2;
                        result += res.Item1;
                    }
                    if (!route.hasFinished)
                    {
                        this.route.arriveAtPoint();
                        switch (next.Item1.type)
                        {
                            case POIType.start:
                                result += "Start: Let\'s go!\n";
                                break;
                            case POIType.terminator:
                                result += route.finish();
                                break;
                            case POIType.gas_station:
                                result += $"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n";
                                result += this.addFuel(next.Item1.value).Item1;
                                break;
                            case POIType.food:
                                result += $"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n";
                                result += "Nom nom, lekker eten\n";
                                break;
                            default:
                                result += $"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n";
                                break;
                        }
                        // Thread.Sleep(1000);
                    }
                }
                result += "We\'re done, getting out of the car..\n\n";
                this.locked = true;
            }
            return showOverride || WorkingParams.showOutput ? result : "";
        }

        public String getIn(String name, Boolean showOverride = false)
        {
            String result = "";
            result += $"Person {name} is trying to get into the car of {this.owner}\n";
            if (name == this.owner)
            {
                this.locked = false;
                result += "    The car is now unlocked!\n";
            }
            else
            {
                result += "    The car doesn't open..\n";
            }
            if (WorkingParams.showOutput || showOverride)
            {
                return result;
            }
            else return "";
        }

        public Tuple<String, int> drive()
        {
            String result = "";
            int distanceDriven = 0;
            if (!this.locked && !this.route.hasFinished && this.fuel > 0)
            {
                Random rnd = new Random();
                switch (this.brand)
                {
                    case Automerken.Audi:
                        // Console.Write("Jaa wir gehen von Vroom Vroom\n");
                        distanceDriven += new Random().Next(3, 7);
                        this.fuel -= new Random().Next(1, 3);
                        break;

                    case Automerken.Ferrari:
                        // Console.Write("Ciao bella, vruomo vruomo!\n");
                        distanceDriven += new Random().Next(4, 8);
                        this.fuel -= new Random().Next(1, 3);
                        break;

                    case Automerken.Mercedes:
                        // Console.Write("Noo noo, this is so not vroom!\n");
                        distanceDriven += new Random().Next(1, 8);
                        this.fuel -= new Random().Next(1, 4);
                        break;
                }
                if (this.fuel <= 0)
                {
                    result += this.route.getStranded();
                }
            }
            this.kmDriven += distanceDriven;
            return Tuple.Create(result, distanceDriven);
        }

        public Tuple<String, bool> checkLock()
        // returns true if the car is locked, returns false if the car is unlocked
        {
            String result = "";
            if (this.locked)
            {
                result += "The car is still locked..\n";
                return Tuple.Create(result, true);
            }
            else
            {
                result += "The car is open!\n";
                return Tuple.Create(result, false);
            }
        }

        public Tuple<String, int> addFuel(int amount)
        {
            String result = "";
            // can't add fuel if the car is locked
            if (!this.locked)
            {
                if (this.fuel < maxFuel / 2)
                {
                    if ((this.fuel + amount >= maxFuel))
                    {
                        this.fuel = maxFuel;
                    }
                    else
                    {
                        this.fuel += amount;
                    }
                    result += "Done, you can now drive some more! " + fuel + " liters left\n";
                }
                result += $"You\'re still good!\nOff you go, with {fuel} liters left!\n";
            }
            // return the fuel amount
            return Tuple.Create(result, this.getFuel());
        }

        public String printSummary(Boolean addToLog = true)
        {
            if (addToLog)
            {
                // returns basic car information and a summary of the trip (so far)
                // Testauto #8's auto (Audi):          6 van de 20 (60 / 225 km)
                String carSummary = $"{this.owner}'s auto ({this.brand}):";
                String routeSummary = $"{this.route.atWaypointNumber} van de {this.route.getLength().Item1} ({this.kmDriven} / {this.route.getLength().Item2} km)";
                while (carSummary.Length < 35)
                {
                    carSummary += " ";
                }
                return $"{carSummary} {routeSummary}\n";
            }
            else
            {
                return "";
            }
        }

    }

}