
public interface IClient
{
    void SendCmd(Cmd cmd);
    void Recvie(Cmd cmd);
}

public interface IServer
{
    void Connect(IClient client);
    void SendCmd(Cmd cmd);
    void Recvie(Cmd cmd);
}


public class NetTest : YanYeek.Singleton<NetTest>, IClient
{
    IServer _server;
    public void Recvie(Cmd cmd)
    {
        // _server.SendCmd();
    }

    public void SendCmd(Cmd cmd)
    {
        // _server.Recvie();
    }
}
