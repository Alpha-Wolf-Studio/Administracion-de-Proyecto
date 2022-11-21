
public class LevelProgression
{
    public static float Life (int idUnit)
    {
        switch (idUnit)
        {
            case 0:
                return 1.3f;
            case 1:
                return 1.09f;
            case 2:
                return 1f;
            case 3:
                return 1f;
        }

        return 1;
    }

    public static float Damage (int idUnit)
    {
        switch (idUnit)
        {
            case 0:
                return 1.13f;
            case 1:
                return 1.04f;
            case 2:
                return 1.01f;
            case 3:
                return 1f;
        }

        return 1;
    }
}