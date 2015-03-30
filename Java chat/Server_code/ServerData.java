package Server;

import java.util.HashMap;
import java.util.Vector;

public class ServerData{
    private HashMap<String,Connection> hm = null;
    private Vector<String> users = null;
    public ServerData(){
        hm = new HashMap<>();
        users = new Vector<>();
    }
    public synchronized Vector<String> getUsers(){return users;}
    public synchronized Connection getConnection(String name){
        return hm.get(name);
    }
    public synchronized void addConnection(Connection c,String name){
        users.add(name);
        hm.put(name, c);
    }
    public synchronized void removeConnection(String name){
        hm.remove(name);
        users.remove(name);
        System.out.println(users);
    }
    public synchronized void changeName(String name,String user_name){
        Connection c = hm.remove(user_name);
        hm.put(name,c);
        users.remove(user_name);
        users.add(name);
    }
}
