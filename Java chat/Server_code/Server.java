package Server;
import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;



public class Server {    
    public static void main(String[] args)throws IOException {
      ServerSocket ss = null;
      Socket cs = null;
      ss = new ServerSocket(1);
      System.out.println("Server pornit!");
      while(true){
      cs = ss.accept();
      System.out.println("Client acceptat!");
      new Client(cs);
      }
    }
}
