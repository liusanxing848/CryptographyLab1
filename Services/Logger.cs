namespace Lab1sharp.Services
{
    public class Logger
    {
        public void Log(string s, string op)
        {
            string content = "";
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            content = formattedTime + ":" + "[" + op + "]" + s;
            StreamWriter writer = new StreamWriter("Logs.txt", true);
            writer.WriteLine(content);
            writer.Close();
        }
    }
}