namespace CRUD
{
    class Product
    {
        static public int productCount;

        public int ID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public string CreationDate { get; set; }

        public Product()
        {
            productCount++;
            ID = productCount;
            Name = "";
            Category = "";
            CreationDate = "";
        }

        public Product(string NameInp, double priceInp, string CatInp, string DateInp)
        {
            productCount++;
            ID = productCount;
            Name = NameInp;
            Price = priceInp;
            Category = CatInp;
            CreationDate = DateInp;
        }
    }
}