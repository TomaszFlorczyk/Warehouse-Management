namespace WarehouseMenagementAPI.Helpers
{
    public class RandomProductGenerator
    {
        private static readonly Random _randomProductGenerator = new Random();

        public static string GenerateRandomProductName()
        {
            string[] possibleNames = { "Meblokart", "Eleganta", "Furnitex", "Stylux", "EkoDesign", "LuksusPlus" };
            return possibleNames[_randomProductGenerator.Next(possibleNames.Length)];
        }

        public static string GenerateRandomProductType()
        {
            string[] possibleTypes = { "Sofa", "Bed", "Wardrobe", "Chair", "Corner", "IKEA" , "Mattress"};
            return possibleTypes[_randomProductGenerator.Next(possibleTypes.Length)];
        }

        public static string GenerateRandomPostalCode()
        {
            int firstPart = _randomProductGenerator.Next(10);
            int secondPart = _randomProductGenerator.Next(1000, 9999);

            return $"{firstPart}{secondPart}";
        }

        public static decimal GenerateRandomProducPrice()
        {
            return Math.Round((decimal)_randomProductGenerator.NextDouble() * 1000, 2);
        }

        public static int GenerateAlleyIdFromPostalCode(string postalCode)
        {
            if(!string.IsNullOrEmpty(postalCode) && char.IsDigit(postalCode[0])) 
            {
                return int.Parse(postalCode[0].ToString());
            }
            return 0;
        }
    }
}