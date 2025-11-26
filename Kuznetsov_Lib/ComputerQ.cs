public class ComputerQ
{
    public string cpuName { get; set; }
    public string cpuValue { get; set; }
    public string ozu { get; set; }

    public ComputerQ(string cpuName, string cpuValue, string ozu)
    {
        this.cpuName = cpuName;
        this.cpuValue = cpuValue;
        this.ozu = ozu;
    }

    public virtual string Q()
    {
        if (string.IsNullOrEmpty(cpuName) ||
            string.IsNullOrEmpty(cpuValue) ||
            string.IsNullOrEmpty(ozu))
        {
            return "Поля пустые";
        }

        int cpuValueInt;
        int ozuInt;

        if (int.TryParse(cpuValue, out cpuValueInt) &&
            int.TryParse(ozu, out ozuInt))
        {
            return ((0.5 * cpuValueInt) + ozuInt).ToString();
        }
        else
        {
            return "Данные должны быть числовыми в строке";
        }
    }
}
