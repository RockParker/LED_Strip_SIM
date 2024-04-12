using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LED_Strip_SIM;

public class RileyPattern : Pattern, IPattern // should always inherit from Pattern and IPattern to ensure the correct methods are implemented
{
    
    public RileyPattern() : base(new List<LedPattern>(){Pattern1,Pattern2})
    {
        _currentPattern = Pattern1;
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

    private static double _iterations = 0;
    private static void Pattern1(ref Rectangle[,] array)
    {
        if(_iterations == 0)
        {
            SetSolid(ref array);
        }
        _iterations+=-.2;
        
        for(int i = 0; i< array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                var led = array[i, j];
                var colour = (led.Fill as SolidColorBrush)!.Color;
                colour.R = (byte) ((Math.Sin(_iterations+ 0) + 1) * 125.0);
                colour.G = (byte) ((Math.Sin(_iterations+ Math.PI) + 1) * 125.0);
                colour.B = (byte) ((Math.Sin(_iterations+ Math.PI/2) + 1) * 125.0);
                
                led.Fill = new SolidColorBrush(colour);

            }
        }

    }
    
    private static void Pattern2(ref Rectangle[,] array)
    {
        
    }
    
    private static void SetSolid(ref Rectangle[,] array)
    {
        for(int i = 0; i < array.GetLength(0); i++)
        {
            for(int j = 0; j < array.GetLength(1); j++)
            {
                var led = array[i, j];
                led.Fill = new SolidColorBrush(Colors.White);
            }
        }
    }
}