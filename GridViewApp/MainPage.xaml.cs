using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GridViewApp
{

    public class MainGridItem
    {
        public string Icon { get; set; }
        public string Title { get; set; }
    }

    public class GridItemManager
    {
        public static List<MainGridItem> GetGridItems()
        {
            var gridItems = new List<MainGridItem>();

            gridItems.Add(new MainGridItem { Icon = "\uE770", Title = "System" });
            gridItems.Add(new MainGridItem { Icon = "\uE772", Title = "Devices" });
            gridItems.Add(new MainGridItem { Icon = "\uEC75", Title = "Phone" });
            gridItems.Add(new MainGridItem { Icon = "\uE774", Title = "Network & Internet" });
            gridItems.Add(new MainGridItem { Icon = "\uE771", Title = "Personalisation" });

            return gridItems;
        }
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            GridItems = GridItemManager.GetGridItems();
        }

        public List<MainGridItem> GridItems;

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
    }

    public class MyGridViewPanel : Panel
    {
        private double _maxWidth;
        private double _maxHeight;
        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            var y = 0.0;
            double width = Window.Current.Bounds.Width;
            if (width <= 720)
            {
                foreach (var child in Children)
                {  
                   
                    var newpos = new Rect(0, y, width-10, _maxHeight);
                    child.Arrange(newpos);
                    y += _maxHeight;
                }
                return finalSize;
            }
            else {
                foreach (var child in Children)
                {
                    if ((_maxWidth + x) > finalSize.Width)
                    {
                        x = 0;
                        y += _maxHeight;
                    }
                    var newpos = new Rect(x, y, _maxWidth, _maxHeight);
                    child.Arrange(newpos);
                    x += _maxWidth;
                }
                return finalSize;
            }
                
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            double width = Window.Current.Bounds.Width;
            if (width <= 720)
            {
                foreach (var child in Children)
                {
                    child.Measure(new Size(width, availableSize.Height));
                    var desiredheight = child.DesiredSize.Height;
                    if (desiredheight > _maxHeight)
                        _maxHeight = desiredheight;
                }
                return new Size(width-10, _maxHeight * Children.Count);
            }
            else {
                foreach (var child in Children)
                {
                    child.Measure(availableSize);
                    var desirtedwidth = child.DesiredSize.Width;
                    if (desirtedwidth > _maxWidth)
                        _maxWidth = desirtedwidth;
                    var desiredheight = child.DesiredSize.Height;
                    if (desiredheight > _maxHeight)
                        _maxHeight = desiredheight;
                }
                var itemperrow = Math.Floor(availableSize.Width / _maxWidth);
                var rows = Math.Ceiling(Children.Count / itemperrow);
                return new Size(itemperrow * _maxWidth, _maxHeight * rows);
            }
            
        }
    }
}
