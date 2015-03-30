package Server;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

public class Connection{
    private Socket cs;
    private DataInputStream is = null;
    private DataOutputStream os = null;
    public Connection(Socket c,DataInputStream i,DataOutputStream o) {
        cs = c;
        is = i;
        os = o;
        
    }
    public void logout() throws IOException  { 
        cs.close();
        is.close();
        os.close();
    }
    public DataInputStream getInputStream(){return is;}
    public DataOutputStream getOutputStream(){return os;}
}

