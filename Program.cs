using Terminal.Gui;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using CRUD;

static class Program
{
    static void AddProductWindow(ScrollView parentWin, Product product, List<Product> products, List<Window> productWindows)
    {
        var productWin = new FixedWindow(product.ID.ToString())
        {
            X = Pos.Percent(25),
            Y = Product.yOffset,
            Width = Dim.Percent(40),
            Height = 13,
            CanFocus = false
        };

        productWindows.Add(productWin);

        var pNameLabel = new Label("Nazwa: ")
        {
            X = 2,
            Y = 1
        };

        var pName = new Label(product.Name)
        {
            X = Pos.Right(pNameLabel),
            Y = Pos.Top(pNameLabel)
        };

        var nameEdit = new TextField(pName.Text.ToString())
        {
            X = Pos.Left(pName),
            Y = Pos.Top(pName),
            Width = 20,
            Visible = false
        };

        var pPriceLabel = new Label("Cena: ")
        {
            X = 2,
            Y = 3
        };

        var pPrice = new Label(product.Price.ToString())
        {
            X = Pos.Right(pPriceLabel),
            Y = Pos.Top(pPriceLabel)
        };

        var priceEdit = new TextField(pPrice.Text.ToString())
        {
            X = Pos.Left(pPrice),
            Y = Pos.Top(pPrice),
            Width = 10,
            Visible = false
        };

        var pCatLabel = new Label("Kategoria: ")
        {
            X = 2,
            Y = 5
        };

        var pCat = new Label(product.Category)
        {
            X = Pos.Right(pCatLabel),
            Y = Pos.Top(pCatLabel)
        };

        var catEdit = new TextField(pCat.Text.ToString())
        {
            X = Pos.Left(pCat),
            Y = Pos.Top(pCat),
            Width = 20,
            Visible = false
        };

        var pDateLabel = new Label("Data utworzenia: ")
        {
            X = 2,
            Y = 7
        };

        var pDate = new Label(product.CreationDate)
        {
            X = Pos.Right(pDateLabel),
            Y = Pos.Top(pDateLabel)
        };

        var dateEdit = new TextField(pDate.Text.ToString())
        {
            X = Pos.Left(pDate),
            Y = Pos.Top(pDate),
            Width = 17,
            Visible = false
        };

        var pEdit = new Button("Edytuj")
        {
            X = 2,
            Y = 9
        };

        var pConfirm = new Button("Zmień")
        {
            X = 2,
            Y = 9,
            Visible = false
        };

        pEdit.Clicked += () =>
        {
            pName.Visible = false;
            nameEdit.Visible = true;
            pPrice.Visible = false;
            priceEdit.Visible = true;
            pCat.Visible = false;
            catEdit.Visible = true;
            pDate.Visible = false;
            dateEdit.Visible = true;
            pEdit.Visible = false;
            pConfirm.Visible = true;
        };
        pConfirm.Clicked += () =>
        {
            if (double.TryParse(priceEdit.Text.ToString(), out double newPrice))
            {
                pName.Text = nameEdit.Text;
                pPrice.Text = priceEdit.Text;
                pCat.Text = catEdit.Text;
                pDate.Text = dateEdit.Text;
                product.Name = nameEdit.Text.ToString();
                product.Price = newPrice;
                product.Category = catEdit.Text.ToString();
                product.CreationDate = dateEdit.Text.ToString();

                pName.Visible = true;
                nameEdit.Visible = false;
                pPrice.Visible = true;
                priceEdit.Visible = false;
                pCat.Visible = true;
                catEdit.Visible = false;
                pDate.Visible = true;
                dateEdit.Visible = false;
                pEdit.Visible = true;
                pConfirm.Visible = false;
            }
        };

        var pDelete = new Button("Usuń")
        {
            X = Pos.Right(pEdit) + 1,
            Y = Pos.Top(pEdit)
        };
        pDelete.Clicked += () =>
        {
            parentWin.Remove(productWin);
            for (int i = productWindows.IndexOf(productWin) + 1; i < productWindows.Count; i++)
            {
                productWindows[i].Y -= 14;
            }
            products.Remove(product);
            parentWin.ContentSize = new Size(parentWin.Frame.Width - 1, products.Count * 14 + 1);
            Product.yOffset -= 14;
        };


        parentWin.Add(productWin);
        productWin.Add(pNameLabel);
        productWin.Add(pName);
        productWin.Add(nameEdit);
        productWin.Add(pPriceLabel);
        productWin.Add(pPrice);
        productWin.Add(priceEdit);
        productWin.Add(pCatLabel);
        productWin.Add(pCat);
        productWin.Add(catEdit);
        productWin.Add(pDateLabel);
        productWin.Add(pDate);
        productWin.Add(dateEdit);
        productWin.Add(pEdit);
        productWin.Add(pConfirm);
        productWin.Add(pDelete);
    }
    static void Main(string[] args)
    {
        CSVHandler csvhandler = new CSVHandler();

        Application.Init();

        List<Product> products = new List<Product>();
        List<Window> productWindows = new List<Window>();

        var top = Application.Top;

        var scroll = new ScrollView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        var bar = new MenuBar
        (
            new MenuBarItem[]
            {
                new MenuBarItem("_Dodaj", new MenuItem[]{ }),
                new MenuBarItem
                (
                    "_Eksportuj(CSV)",
                    new MenuItem[]
                    {
                        new MenuItem("_Eksport", "", () =>
                        {
                            csvhandler.ExportCSV(products);
                        })
                    }
                )
            }
        );

        FixedWindow dropdown = null;

        bar.MouseClick += (args) =>
        {
            var mouseX = args.MouseEvent.X;

            if (dropdown != null)
            {
                top.Remove(dropdown);
                dropdown = null;
            }
            else if (mouseX >= 0 && mouseX <= 6)
            {

                dropdown = new FixedWindow("Input")
                {
                    X = 0,
                    Y = 1,
                    Width = 30,
                    Height = 11,
                    CanFocus = false
                };

                var nameLabel = new Label("Nazwa: ")
                {
                    X = 2,
                    Y = 1
                };

                var nameInp = new TextField()
                {
                    X = Pos.Right(nameLabel),
                    Y = Pos.Top(nameLabel),
                    Width = 18,
                };

                var priceLabel = new Label("Cena: ")
                {
                    X = 2,
                    Y = 3
                };

                var priceInp = new TextField()
                {
                    X = Pos.Right(priceLabel),
                    Y = Pos.Top(priceLabel),
                    Width = 10
                };

                var catLabel = new Label("Kategoria: ")
                {
                    X = 2,
                    Y = 5
                };

                var catInp = new TextField()
                {
                    X = Pos.Right(catLabel),
                    Y = Pos.Top(catLabel),
                    Width = 18
                };

                var addProduct = new Button("Dodaj")
                {
                    X = Pos.Percent(33),
                    Y = 7,
                };
                addProduct.Clicked += () =>
                {
                    if (double.TryParse(priceInp.Text.ToString(), out double priceI))
                    {
                        products.Add(new Product(nameInp.Text.ToString(), priceI, catInp.Text.ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                        AddProductWindow(scroll, products[^1], products, productWindows, yOffset);
                        top.Remove(dropdown);
                        dropdown = null;
                    }
                };

                dropdown.Add(nameLabel);
                dropdown.Add(nameInp);
                dropdown.Add(priceLabel);
                dropdown.Add(priceInp);
                dropdown.Add(catLabel);
                dropdown.Add(catInp);
                dropdown.Add(addProduct);
                top.Add(dropdown);
            }

        };

        var quitButton = new Button("_X")
        {
            X = Pos.AnchorEnd(1) - 4,
            Y = 0
        };

        quitButton.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Red),
            Focus = Application.Driver.MakeAttribute(Color.White, Color.Red)
        };

        quitButton.Clicked += () => Application.RequestStop();

        var win = new Window("CRUD")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        
        scroll.LayoutComplete += (args) =>
        {
            scroll.ContentSize = new Size(scroll.Frame.Width - 1, Product.yOffset);
        };

        csvhandler.ReadCSV(products);

        top.Add(bar);
        top.Add(quitButton);
        top.Add(win);
        win.Add(scroll);

        for(int i = 0; i < products.Count; i++)
        {
            AddProductWindow(scroll, products[i], products, productWindows, yOffset);
        }

        Application.RootMouseEvent += (args) => { };
        Application.Run();
        Application.Shutdown();
    }
}

class FixedWindow : Window
{
    public FixedWindow(string title) : base(title) { }

    public override bool MouseEvent(MouseEvent me)
    {
        foreach (var view in Subviews)
        {
            if (view.MouseEvent(me))
            {
                return true;
            }
        }
        return false;
    }
}