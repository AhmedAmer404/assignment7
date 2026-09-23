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

    public struct Shipment
    {
        private string trackingCode;
        private string description;

        private double weight;

        private decimal deliveryFee;

        public DeliveryAddress Destination
        {
            get;
            set;
        }

        public string TrackingCode
        {
            get
            {
                return trackingCode;
            }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    trackingCode = value;
                }

            }
        }
        public string Description
        {
            get
            {
                return description;

            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    description = value;
                }

            }

        }
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value > 0)
                {
                    weight = value;
                }
            }
        }
        public decimal DeliveryFee
        {
            get
            {
                return deliveryFee;
            }
            private set
            {
                if (value > 0)
                {
                    deliveryFee = value;
                }
            }
        }
        public double EstimatedCost
        {
            get
            {
                return 5 * Weight + (double)DeliveryFee;
            }
        }
        public Shipment(string trackingCode)
        {
            this.trackingCode = "Unknown";
            this.description = "Unknown";
            this.weight = 1;
            this.deliveryFee = 50;
            Destination = new DeliveryAddress();
        }
        public Shipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination)
        {
            this.trackingCode = "Unknown";
            this.description = "Unknown";
            this.weight = 1;
            this.deliveryFee = 50;
            Destination = destination;

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
        public void PrintShipment()
        {

            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description   : {Description}");
            Console.WriteLine($"Weight        : {Weight}");
            Console.WriteLine($"Delivery Fee  : {DeliveryFee}");
            Console.WriteLine($"Destination   : {Destination.GetFullAddress()}");
            Console.WriteLine($"Estimated Cost: {EstimatedCost}");

        }
    }
    #region Question 1
    struct DeliveryCenter
    { 
        private Shipment[] shipments;

        public DeliveryCenter(int capacity = 10)
        {
            shipments = new Shipment[capacity];
        }
        public Shipment this[int index]
        {
            get
            {
                if (index >= 0 && index < shipments.Length)
                {
                    return shipments[index];
                }
                return default;
            }
            set
            {
                if(index >= 0 && index < shipments.Length)
                {
                    shipments[index] = value;
                }
            }
        }
        public Shipment this[string index]
        {
            get
            {
                foreach (var shipment in shipments)
                {
                    if (!string.IsNullOrWhiteSpace(shipment.TrackingCode) && shipment.TrackingCode == index)
                    {
                        return shipment;
                    }
                }
                return default;
            }
        }
        public  bool AddShipment(Shipment shipment)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(shipments[i].TrackingCode))
                {
                    shipments[i] = shipment;
                    return true;
                }
            }

            return false;
        }

    }

    #endregion
}
