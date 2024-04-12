using System.Collections.Generic;
using System.Windows.Shapes;

namespace LED_Strip_SIM;

public abstract class Pattern
{
    protected delegate void LedPattern(ref Rectangle[,] ledArray);
    protected LedPattern _currentPattern;
    protected readonly List<LedPattern> _delegates;
    protected int _currentPatternIndex = 0;

    protected Pattern(List<LedPattern> patterns)
    {
            _delegates = patterns;
            _currentPattern = patterns[0];
    }
    
    public void RunPattern(ref Rectangle[,] ledArray)
    {
        _currentPattern(ref ledArray);
    }
}