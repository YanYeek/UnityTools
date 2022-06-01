
/// <summary>
/// 消息基类
/// </summary>
public class Cmd
{

}

public class LogonCmd : Cmd
{
    public string account;
    public string password;
}