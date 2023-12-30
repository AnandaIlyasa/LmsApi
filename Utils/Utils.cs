namespace LmsApi.Utils;

static class Utils
{
    public static string GenerateRandomAlphaNumericUtil()
    {
        Random rand = new Random();

        int letterLength = 3;
        int randomDigit;
        string code = "";
        char letter;
        for (int i = 0; i < letterLength; i++)
        {
            randomDigit = rand.Next(0, 26);
            letter = Convert.ToChar(randomDigit + 65);
            code = code + letter;
        }

        randomDigit = rand.Next(10, 99);
        code = code + randomDigit;

        return code;
    }
}