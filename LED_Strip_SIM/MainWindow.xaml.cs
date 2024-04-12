using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LED_Strip_SIM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int LedPerStrip = 12;
        private const int StripCount = 12;
        private const int LedPixelSize = 20;
        
        private readonly DispatcherTimer _timer;
        private Rectangle[,] _leds;
        
        private int TickRate => 500 - (int)SpeedSlider.Value;

        public MainWindow()
        {
            InitializeComponent();
            InitLeds();
            
            
            var patternHost = new VerticalPatterns();

            _timer = new DispatcherTimer(); //  dispatcher timer runs on the UI Thread
            _timer.Tick += (sender, args) =>
            {
                patternHost.RunPattern(ref _leds);
            };
            _timer.Start();

            SpeedSlider.Value = 150;
        }

        /// <summary>
        /// Initializes the rectangle array and sets a default fill color based on the strip
        /// </summary>
        private void InitLeds()
        {
            _leds = new Rectangle[StripCount, LedPerStrip];

            for (int i = 0; i < StripCount; i++)
            {
                var strip = new StackPanel()        // creating the new strip
                {
                    Name = "Strip" + i
                };
                
                for (int j = 0; j < LedPerStrip; j++)
                {
                    _leds[i, j] = new Rectangle()   // START OF RECTANGLE
                    {
                        Height = LedPixelSize,
                        Width = LedPixelSize,
                        Fill = new SolidColorBrush(Color.FromRgb((byte)(i * 20), (byte)(i * 20), (byte)(i * 20))),
                        Margin = new Thickness(4),
                        Tag = $"Strip: {i},\tLED:{j}"
                    };                              // END OF RECTANGLE
                    
                    _leds[i, j].MouseEnter += (s, e) => MouseOverLabel.Content = (s as Rectangle)!.Tag;
                    
                    strip.Children.Add(_leds[i, j]);
                    
                }
                
                Sign.Children.Add(strip);           // adding the strip to the canvas
            }
        }
        
        
        /*
         * ========================================
         *          UI Event Handlers Below
         * ========================================
         */
        

        ///<summary>Stops and Starts the Dispatcher Timer</summary>
        private void StopStartPattern(object sender, EventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                Stopper.Content = "Start Pattern";
            }
            else
            {
                _timer.Start();
                Stopper.Content = "Stop Pattern";
            }

        }

        private void SpeedChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _timer.Interval     = TimeSpan.FromMilliseconds(TickRate);
            SliderLabel.Content = $"Tick Rate: {TickRate}ms";
        }
    }
}