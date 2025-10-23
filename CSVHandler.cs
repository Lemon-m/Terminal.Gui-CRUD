namespace CRUD
{
    class CSVHandler
    {
        static public void ReadCSV(List<Product> products)
        {
            if (File.Exists("produkty.csv"))
            {
                var lines = File.ReadAllLines("produkty.csv");

                if (lines.Length > 1)
                {
                    int lastID = 0;

                    for (int i = 1; i < lines.Length; i++)
                    {
                        var cols = lines[i].Split(';');
                        if (int.TryParse(cols[0], out int id))
                        {
                            if (id > lastID) lastID = id;
                        }

                        if (double.TryParse(cols[2], out double priceCSVTemp))
                        {
                            products.Add(new Product(cols[1], priceCSVTemp, cols[3], cols[4]));
                        }
                    }

                    Product.productCount = lastID;
                }
            }
            else
            {
                Product.productCount = 0;
            }
        }
    
        static public void ExportCSV(List<Product> products)
        {
            File.WriteAllText("produkty.csv", "ID;Nazwa;Cena;Kategoria;Data utworzenia" + Environment.NewLine);
            using (StreamWriter writer = File.AppendText("produkty.csv"))
            {
                foreach (Product pdt in products)
                {
                    writer.WriteLine($"{pdt.ID};{pdt.Name};{pdt.Price};{pdt.Category};{pdt.CreationDate}");
                }
            }
        }
    }
}