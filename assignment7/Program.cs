using System;
namespace Assignment7
{

    public struct DeliveryAddress
    {
        public string City;
        public string Street;

        public int BuildingNumber;

        public DeliveryAddress(string city, string street, int buildingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        public String GetFullAddress()
        {
            return $"{Street} {BuildingNumber}, {City}";
        }

    }


    ////  #region Question 1
    //  struct DeliveryCenter
    //  { 
    //      private Shipment[] shipments;

    //      public DeliveryCenter(int capacity = 10)
    //      {
    //          shipments = new Shipment[capacity];
    //      }
    //      public Shipment this[int index]
    //      {
    //          get
    //          {
    //              if (index >= 0 && index < shipments.Length)
    //              {
    //                  return shipments[index];
    //              }
    //              return default;
    //          }
    //          set
    //          {
    //              if(index >= 0 && index < shipments.Length)
    //              {
    //                  shipments[index] = value;
    //              }
    //          }
    //      }
    //      public Shipment this[string index]
    //      {
    //          get
    //          {
    //              foreach (var shipment in shipments)
    //              {
    //                  if (!string.IsNullOrWhiteSpace(shipment.TrackingCode) && shipment.TrackingCode == index)
    //                  {
    //                      return shipment;
    //                  }
    //              }
    //              return default;
    //          }
    //      }
    //      public  bool AddShipment(Shipment shipment)
    //      {
    //          for (int i = 0; i < shipments.Length; i++)
    //          {
    //              if (string.IsNullOrWhiteSpace(shipments[i].TrackingCode))
    //              {
    //                  shipments[i] = shipment;
    //                  return true;
    //              }
    //          }

    //          return false;
    //      }

    //  }

    //  #endregion


    #region Question 2
    public class Shipment
    {
        private string trackingCode;
        private string description;
        private decimal weight;
        private decimal deliveryFee;

        public string TrackingCode
        {
            get { return trackingCode; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    trackingCode = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    description = value;
            }
        }

        public decimal Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
            }
        }

        public decimal DeliveryFee
        {
            get { return deliveryFee; }
            private set
            {
                if (value > 0)
                    deliveryFee = value;
            }
        }

        public DeliveryAddress Destination { get; set; }


        
        public virtual decimal EstimatedCost
        {
            get
            {
                return DeliveryFee + (Weight * 5);
            }
        }


        
        public Shipment(string trackingCode)
        {
            TrackingCode = trackingCode;

            Description = "Unknown";
            Weight = 1;
            DeliveryFee = 50;

            Destination = new DeliveryAddress(
                "Unknown",
                "Unknown",
                0
            );
        }


        
        public Shipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
            Destination = destination;
        }


        public void UpdateDeliveryFee(decimal newFee)
        {
            if (newFee > 0)
            {
                DeliveryFee = newFee;
            }
        }


        public virtual void PrintShipment()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Type          : {GetType().Name}");
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description   : {Description}");
            Console.WriteLine($"Weight        : {Weight}");
            Console.WriteLine($"Delivery Fee  : {DeliveryFee}");
            Console.WriteLine($"Destination   : {Destination.GetFullAddress()}");
            Console.WriteLine($"Estimated Cost: {EstimatedCost}");
            Console.WriteLine("--------------------------------");
        }
    }
 

    #endregion

    #region Question 3
    public class StandardShipment : Shipment
    {
        public StandardShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination)
            : base(
                trackingCode,
                description,
                weight,
                deliveryFee,
                destination)
        {
        }
    }
    public class ExpressShipment : Shipment
    {
        private decimal extraFee;

        public decimal ExtraFee
        {
            get { return extraFee; }
            set
            {
                if (value >= 0)
                    extraFee = value;
            }
        }


        public override decimal EstimatedCost
        {
            get
            {
                return DeliveryFee + (Weight * 5) + ExtraFee;
            }
        }


        public ExpressShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination,
            decimal extraFee)
            : base(
                trackingCode,
                description,
                weight,
                deliveryFee,
                destination)
        {
            ExtraFee = extraFee;
        }


        public override void PrintShipment()
        {
            base.PrintShipment();

            Console.WriteLine($"Extra Fee     : {ExtraFee}");
        }
        public class InternationalShipment : Shipment
        {
            private string destinationCountry;
            private decimal customsFee;


            public string DestinationCountry
            {
                get { return destinationCountry; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        destinationCountry = value;
                }
            }


            public decimal CustomsFee
            {
                get { return customsFee; }
                set
                {
                    if (value >= 0)
                        customsFee = value;
                }
            }


            public override decimal EstimatedCost
            {
                get
                {
                    return DeliveryFee + (Weight * 5) + CustomsFee;
                }
            }


            public InternationalShipment(
                string trackingCode,
                string description,
                decimal weight,
                decimal deliveryFee,
                DeliveryAddress destination,
                string destinationCountry,
                decimal customsFee)
                : base(
                    trackingCode,
                    description,
                    weight,
                    deliveryFee,
                    destination)
            {
                DestinationCountry = destinationCountry;
                CustomsFee = customsFee;
            }


            public override void PrintShipment()
            {
                base.PrintShipment();

                Console.WriteLine($"Country       : {DestinationCountry}");
                Console.WriteLine($"Customs Fee   : {CustomsFee}");
            }
        }




    }

    #endregion

    #region Question 4
    public class DeliveryCenter
    {
        private Shipment[] shipments;

        public string CenterName { get; set; }


        public DeliveryCenter(string centerName)
        {
            CenterName = centerName;
            shipments = new Shipment[20];
        }


        
        public Shipment this[int index]
        {
            get
            {
                if (index >= 0 && index < shipments.Length)
                    return shipments[index];

                return null;
            }

            set
            {
                if (index >= 0 && index < shipments.Length)
                    shipments[index] = value;
            }
        }


        
        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null &&
                        shipments[i].TrackingCode == trackingCode)
                    {
                        return shipments[i];
                    }
                }

                return null;
            }
        }


       
        public bool AddShipment(Shipment shipment)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] == null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }

            return false;
        }


        
        public bool RemoveShipment(string trackingCode)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null &&
                    shipments[i].TrackingCode == trackingCode)
                {
                    shipments[i] = null;
                    return true;
                }
            }

            return false;
        }


        
        public void PrintAllShipments()
        {
            Console.WriteLine($"\n===== {CenterName} =====");

            bool found = false;

            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null)
                {
                    shipments[i].PrintShipment();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No shipments available.");
            }
        }
    }


    #endregion

    class Program
    {
        static void Main()
        {
            
            Console.Write("Enter Delivery Center Name: ");
            string centerName = Console.ReadLine();

            DeliveryCenter center =
                new DeliveryCenter(centerName);


          

            Console.WriteLine("\n===== Standard Shipment =====");

            Console.Write("Tracking Code: ");
            string standardTracking = Console.ReadLine();

            Console.Write("Description: ");
            string standardDescription = Console.ReadLine();

            Console.Write("Weight: ");
            decimal standardWeight =
                decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal standardFee =
                decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string standardCity = Console.ReadLine();

            Console.Write("Street: ");
            string standardStreet = Console.ReadLine();

            Console.Write("Building Number: ");
            int standardBuilding =
                int.Parse(Console.ReadLine());

            DeliveryAddress standardAddress =
                new DeliveryAddress(
                    standardCity,
                    standardStreet,
                    standardBuilding);


            StandardShipment standardShipment =
                new StandardShipment(
                    standardTracking,
                    standardDescription,
                    standardWeight,
                    standardFee,
                    standardAddress);


            center.AddShipment(standardShipment);


            

            Console.WriteLine("\n===== Express Shipment =====");

            Console.Write("Tracking Code: ");
            string expressTracking = Console.ReadLine();

            Console.Write("Description: ");
            string expressDescription = Console.ReadLine();

            Console.Write("Weight: ");
            decimal expressWeight =
                decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal expressFee =
                decimal.Parse(Console.ReadLine());

            Console.Write("Extra Fee: ");
            decimal extraFee =
                decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string expressCity = Console.ReadLine();

            Console.Write("Street: ");
            string expressStreet = Console.ReadLine();

            Console.Write("Building Number: ");
            int expressBuilding =
                int.Parse(Console.ReadLine());

            DeliveryAddress expressAddress =
                new DeliveryAddress(
                    expressCity,
                    expressStreet,
                    expressBuilding);


            ExpressShipment expressShipment =
                new ExpressShipment(
                    expressTracking,
                    expressDescription,
                    expressWeight,
                    expressFee,
                    expressAddress,
                    extraFee);


            center.AddShipment(expressShipment);


            

            Console.WriteLine("\n===== International Shipment =====");

            Console.Write("Tracking Code: ");
            string internationalTracking =
                Console.ReadLine();

            Console.Write("Description: ");
            string internationalDescription =
                Console.ReadLine();

            Console.Write("Weight: ");
            decimal internationalWeight =
                decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal internationalFee =
                decimal.Parse(Console.ReadLine());

            Console.Write("Destination Country: ");
            string country = Console.ReadLine();

            Console.Write("Customs Fee: ");
            decimal customsFee =
                decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string internationalCity =
                Console.ReadLine();

            Console.Write("Street: ");
            string internationalStreet =
                Console.ReadLine();

            Console.Write("Building Number: ");
            int internationalBuilding =
                int.Parse(Console.ReadLine());


            DeliveryAddress internationalAddress =
                new DeliveryAddress(
                    internationalCity,
                    internationalStreet,
                    internationalBuilding);


            InternationalShipment internationalShipment =
                new InternationalShipment(
                    internationalTracking,
                    internationalDescription,
                    internationalWeight,
                    internationalFee,
                    internationalAddress,
                    country,
                    customsFee);


            center.AddShipment(internationalShipment);


            

            center.PrintAllShipments();


            Console.Write("\nEnter Tracking Code to Search: ");
            string searchCode = Console.ReadLine();

            Shipment foundShipment = center[searchCode];

            if (foundShipment != null)
            {
                Console.WriteLine("\nShipment Found:");
                foundShipment.PrintShipment();
            }
            else
            {
                Console.WriteLine("Shipment not found.");
            }


         

            Console.Write("\nEnter Tracking Code to Remove: ");
            string removeCode = Console.ReadLine();

            bool removed = center.RemoveShipment(removeCode);

            if (removed)
            {
                Console.WriteLine("Shipment removed successfully.");
            }
            else
            {
                Console.WriteLine("Shipment not found.");
            }


            

            Console.WriteLine("\n===== Remaining Shipments =====");

            center.PrintAllShipments();
        }
    }
}
