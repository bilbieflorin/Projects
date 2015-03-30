
package Server;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;


public class Client extends Thread{
    private static ServerData server=new ServerData();
    private String user_name = null;
    public Client(Socket cs)throws IOException{
        DataInputStream i = new DataInputStream(cs.getInputStream());
        DataOutputStream o = new DataOutputStream(cs.getOutputStream());
        o.writeUTF("@Server: Introduceti numele de utilizator(numele nu trebuie sa inceapa cu '@'):");
        String s = i.readUTF(); 
        while(server.getUsers().contains(s) || s.charAt(0)=='@'){
            o.writeUTF("@Server: Nume existent sau eronat! Reintroduceti!");
            s = i.readUTF();
        }
        o.writeUTF("@Server: Nume acceptat!");
        Connection client=new Connection(cs,i,o);
        server.addConnection(client, s);
        user_name = s;
        start();
    }
    @Override
    public void run(){
        Connection c = server.getConnection(user_name);
        try{
        String cmd = null;
        do{
        cmd = c.getInputStream().readUTF();
        switch(cmd){
            case "MSG":{
                String name = c.getInputStream().readUTF();
                String msg = c.getInputStream().readUTF();
                Connection cx = null;
                cx = server.getConnection(name);
                if(cx==null)
                    c.getOutputStream().writeUTF("@Server: Utilizator inexistent!");
                else{
                    cx.getOutputStream().writeUTF(user_name+": "+msg);
                    c.getOutputStream().writeUTF("@Server: Mesaj expediat!");
                }
                break;
                }
            case "LIST":{ 
                String list="@Server: ";
                for(String user : server.getUsers())
                    list = list+user+" ";
                c.getOutputStream().writeUTF(list);
                break;
            }
            case "BCAST":{
                c.getOutputStream().writeUTF("@Server: Introduceti mesajul dumneavoastra!");
                String msg = c.getInputStream().readUTF();
                for(String user : server.getUsers())
                    if(!user.equals(user_name))
                        server.getConnection(user).getOutputStream().writeUTF(user_name+": "+msg);
                break;
            }
            case "NICK":{
                c.getOutputStream().writeUTF("@Server: Introduceti noul nume dorit!");
                String name = c.getInputStream().readUTF();
                if(!server.getUsers().contains(name)){
                    server.changeName(name, user_name);
                    user_name = name;
                    c.getOutputStream().writeUTF("@Sever: Nume modificat!");
                }
                else
                    c.getOutputStream().writeUTF("@Server: Nume existent! Procedura nereusita!");
                break;
            }
            case "QUIT" :  c.getOutputStream().writeUTF("@Server: La revedere " + user_name+"!");
                           break; 
        }
        }
        while(!cmd.equals("QUIT"));
    }
        catch(IOException e){}
        try{
        c.logout();
        server.removeConnection(user_name);
        }
        catch(Exception e){}
        System.out.println("Client deconectat");
        }
}