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

        public void go()
        {
            Console.Write($"{this.owner} is starting the tour..\n");
            if (!checkLock())
            {
                while (!route.hasFinished)
                {
                    var next = route.getNextPoint();
                    int distanceToNext = next.Item2;
                    while (distanceToNext > 0 && !this.route.hasFinished)
                    {
                        distanceToNext -= this.drive();
                    }
                    if (!route.hasFinished)
                    {
                        this.route.arriveAtPoint();
                        switch (next.Item1.type)
                        {
                            case POIType.start:
                                Console.Write("Start: Let\'s go!");
                                break;
                            case POIType.terminator:
                                Console.Write("You've finished!");
                                route.finish();
                                break;
                            case POIType.gas_station:
                                Console.Write($"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n");
                                this.addFuel(next.Item1.value);
                                break;
                            case POIType.food:
                                Console.Write($"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n");
                                Console.Write("Nom nom, lekker eten");
                                break;
                            default:
                                Console.Write($"Arrived at waypoint {next.Item1.name} at {next.Item2}km!\nFuel left: {this.fuel}\n");
                                break;
                        }
                        // Thread.Sleep(1000);
                    }
                }
                Console.Write("We\'re done, getting out of the car..\n");
                this.locked = true;
            }
        }

        public String getIn(String name)
        {
            Console.Write($"Person {name} is trying to get into the car of {this.owner}\n");
            if (name == this.owner)
            {
                this.locked = false;
                return ("    The car is now unlocked!");
            }
            else
            {
                return ("    The car doesn't open..");
            }
        }

        public int drive()
        {
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
                        if (this.fuel <= 0)
                        {
                            Console.Write("Stranded..\n");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Ferrari:
                        // Console.Write("Ciao bella, vruomo vruomo!\n");
                        distanceDriven += new Random().Next(4, 8);
                        this.fuel -= new Random().Next(1, 3);
                        if (this.fuel <= 0)
                        {
                            Console.Write("Stranded..\n");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Mercedes:
                        // Console.Write("Noo noo, this is so not vroom!\n");
                        distanceDriven += new Random().Next(1, 8);
                        this.fuel -= new Random().Next(1, 4);
                        if (this.fuel <= 0)
                        {
                            Console.Write("Stranded..\n");
                            this.route.getStranded();
                        }
                        break;
                }
            }
            this.kmDriven += distanceDriven;
            return distanceDriven;
        }

        public bool checkLock()
        // returns true if the car is locked, returns false if the car is unlocked
        {
            if (this.locked)
            {
                Console.Write("The car is still locked..\n");
                return true;
            }
            else
            {
                Console.Write("The car is open!\n");
                return false;
            }
        }

        public int addFuel(int amount)
        {
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
                    Console.Write("Done, you can now drive some more! " + fuel + " liters left\n");
                }
                Console.Write($"You\'re still good!\nOff you go, with {fuel} liters left!\n");
            }
            // return the fuel amount
            return this.getFuel();
        }

        public String printSummary()
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
    }

}