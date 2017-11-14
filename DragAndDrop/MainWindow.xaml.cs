using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using FileHelpers;


namespace DragAndDrop
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isRectDragInProg;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = true;
            rect.CaptureMouse();
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            rect.ReleaseMouseCapture();
            var engine = new FileHelperEngine<Orders>();

            var orders = new List<Orders>();

            
            double x = Canvas.GetLeft(rect);
            int x1 = Convert.ToInt32(x);
            double y = Canvas.GetTop(rect);
            int y1 = Convert.ToInt32(y);
            orders.Add(new Orders()
            {
                x = x1,
                y = y1
            });
            engine.WriteFile("Output.Txt", orders);
            Console.WriteLine("X: " + x + " Y: " + y);
        }

        private void rect_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);

            // center the rect on the mouse
            double left = mousePos.X - (rect.ActualWidth / 2);
            double top = mousePos.Y - (rect.ActualHeight / 2);
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            
        }
        [DelimitedRecord("|")]
        public class Orders
        {
            public int x;
            public int y;
        }
    }
    
}
