using Terminal.Gui;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using CRUD;

static class Program
{
    
    static void Main(string[] args)
    {
        Application.Init();

        List<Product> products = new List<Product>();
        List<Window> productWindows = new List<Window>();

        UI ui = new UI(products);

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