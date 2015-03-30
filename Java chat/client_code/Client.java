
package client;

import java.net.*; import java.util.*; import java.io.*;

class Client {
  public static void main(String[] args) {
    Socket cs;
    try{
    Scanner sc = new Scanner(System.in);
    cs = new Socket( "localhost", 1);
    DataOutputStream os = new DataOutputStream(cs.getOutputStream());
    DataInputStream is = new DataInputStream(cs.getInputStream());
    String in ;
    in = is.readUTF();
    System.out.println(in);
    do {
       os.writeUTF(sc.next());
       in = is.readUTF();
       System.out.println(in);
    }
    while(!in.equals("@Server: Nume acceptat!"));
    System.out.println("Conectare reusita!");
    String cmd;
    do{
        System.out.println("Comanda :");
        cmd= sc.next();
        switch(cmd){
            case "LIST": {
                String out;
                os.writeUTF(cmd);
                out = is.readUTF();
                while(out.charAt(0)!='@') {
                    System.out.println(out);
                    out = is.readUTF();
                    }
                Scanner scd = new Scanner(out).useDelimiter("[ ]");
                System.out.println(scd.next()+"Ultilizatorii conectati sunt: ");
                while(scd.hasNext())
                    System.out.println(scd.next());
                break;
            }
            case "MSG":{ 
                String out;
                os.writeUTF(cmd);
                System.out.println("Dati numele destinatarului si mesajul:");
                String dest = sc.next();
                os.writeUTF(dest);
                System.out.println();
                String msg = sc.nextLine();
                os.writeUTF(msg);
                 do {
                    out = is.readUTF();
                    System.out.println(out);
                    }
                while(out.charAt(0)!='@');
                break;
            }
            case "BCAST": {
                os.writeUTF(cmd);
                String out;
                 do {
                    out = is.readUTF();
                    System.out.println(out);
                    }
                while(out.charAt(0)!='@');
                os.writeUTF(sc.next()+sc.nextLine());
                break;
            }
            case "NICK":{
                String out;
                os.writeUTF(cmd);
                do {
                    out = is.readUTF();
                    System.out.println(out);
                    }
                while(out.charAt(0)!='@');
                os.writeUTF(sc.next());
                 do {
                    out = is.readUTF();
                    System.out.println(out);
                    }
                while(out.charAt(0)!='@');
                break;
            }
            case "QUIT":{
                os.writeUTF(cmd);
                String out;
                do {
                    out = is.readUTF();
                    System.out.println(out);
                    }
                while(out.charAt(0)!='@');
                break;
            }   
            default : System.out.println("Comanda gresita!!!");    
        }
        
    }
    while(!cmd.equals("QUIT"));
    cs.close();
    os.close();
    is.close();
  }
    catch(IOException e){
        System.out.println("Conexiune esuata!");
 }
  }
  
}