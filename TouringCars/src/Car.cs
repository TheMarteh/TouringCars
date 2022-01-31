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
            this.fuel = WorkingParams.startingFuel;
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
            Console.WriteLine($"{this.owner} is starting the tour..");
            if (!checkLock())
            {
                while (!route.hasFinished)
                {
                    PointOfInterest next = route.getNextPoint();
                    while (this.kmDriven < next.location && !this.route.hasFinished)
                    {
                        this.drive();
                    }
                    if (!route.hasFinished)
                    {
                        this.route.arriveAtPoint();
                        switch (next.type)
                        {
                            case POIType.terminator:
                                Console.WriteLine("You've finished!");
                                route.finish();
                                break;
                            case POIType.gas_station:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                this.addFuel(next.value);
                                break;
                            case POIType.food:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                Console.WriteLine("Nom nom, lekker eten");
                                break;
                            default:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                break;
                        }
                        // Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("We\'re done, getting out of the car..\n");
                this.locked = true;
            }
        }

        public void getIn(String name)
        {
            Console.WriteLine($"Person {name} is trying to get into the car of {this.owner}");
            if (name == this.owner)
            {
                Console.WriteLine("    The car is now unlocked!");
                this.locked = false;
            }
            else
            {
                Console.WriteLine("    The car doesn't open..");
            }
        }

        public void drive()
        {
            if (!this.locked && !this.route.hasFinished && this.fuel > 0)
            {
                Random rnd = new Random();
                switch (this.brand)
                {
                    case Automerken.Audi:
                        // Console.WriteLine("Jaa wir gehen von Vroom Vroom\n");
                        this.kmDriven += new Random().Next(3, 7);
                        this.fuel -= new Random().Next(1, 3);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Ferrari:
                        // Console.WriteLine("Ciao bella, vruomo vruomo!\n");
                        this.kmDriven += new Random().Next(4, 8);
                        this.fuel -= new Random().Next(1, 3);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Mercedes:
                        // Console.WriteLine("Noo noo, this is so not vroom!\n");
                        this.kmDriven += new Random().Next(1, 8);
                        this.fuel -= new Random().Next(1, 4);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;
                }
            }
        }

        public bool checkLock()
        // returns true if the car is locked, returns false if the car is unlocked
        {
            if (this.locked)
            {
                return true;
            }
            else
            {
                Console.WriteLine("The car is still locked.. Please unlock first!");
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
                    Console.WriteLine("Done, you can now drive some more! " + fuel + " liters left");
                }
                Console.WriteLine($"You\'re still good!\nOff you go, with {fuel} liters left!");
            }
            // return the fuel amount
            return this.getFuel();
        }

        public bool checkFuel()
        {
            if (this.fuel > 10)
            {
                // no need to refuel
                return false;
            }
            else
            {
                // we need to refuel
                return true;
            }
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