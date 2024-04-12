using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LED_Strip_SIM;

public class VerticalPatterns : Pattern, IPattern // should always inherit from Pattern and IPattern to ensure the correct methods are implemented
{
    public VerticalPatterns() : base(new List<LedPattern> { Pattern1, Pattern2}) // uses base to set the patterns
    {
        _currentPattern = Pattern2;
    }

    public void CyclePattern() // defines the pattern cycle
    {
        switch (_currentPatternIndex)        
        {
            case 0:
            {
                _currentPattern = Pattern1;
                break;
            }
            case 1:
            {
                _currentPattern = Pattern2;
                break;
            }
        }
        
        _currentPatternIndex = ++_currentPatternIndex % _delegates.Count;
    }
    
    private static void Pattern1(ref Rectangle[,] ledArray) // example of a pattern
    {
        for (int i = 0; i < ledArray.GetLength(0); i++) // iterates the strips
        {
            for (int j = 0; j < ledArray.GetLength(1) ; j++) // iterates the leds
            {
                var led = ledArray[i, j];
                var colour = (led.Fill as SolidColorBrush)!.Color;
                colour.R = (byte)((colour.R + 15) % 254);
                led.Fill = new SolidColorBrush(colour);
            }
        }
    }

    private static int _previousRow = 0;
    private static void Pattern2(ref Rectangle[,] ledArray) // uses the previous row to determine the next row
    {
        
        for (int j = 0; j < ledArray.GetLength(0) ; j++)
        {
            var led = ledArray[j, _previousRow];
            var colour = (led.Fill as SolidColorBrush)!.Color;
            colour.R = (byte)((colour.R + 15) % 254);
            led.Fill = new SolidColorBrush(colour);
        }
        
        _previousRow = ++_previousRow % ledArray.GetLength(1);
    }
}