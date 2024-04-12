using System.Windows.Shapes;

namespace LED_Strip_SIM;

public interface IPattern
{
    public delegate void Pattern( ref Rectangle[,] ledArray );

    public void RunPattern(ref Rectangle[,] ledArray);

    public void CyclePattern();
}