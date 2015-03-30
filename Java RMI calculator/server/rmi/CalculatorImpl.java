package  rmi;
import java.rmi.*;
import java.rmi.server.*;

public class CalculatorImpl extends UnicastRemoteObject 
               implements CalculatorInterface {
  private double memory = 0.0;
  private double curent = 0.0;

  public CalculatorImpl() throws RemoteException { 
  }

    @Override
    public double adunare(double a) throws RemoteException {
        return (curent+=a);     
    }

    @Override
    public double scadere(double a) throws RemoteException {
        return (curent-=a);
    }

    @Override
    public double inmultire(double a) throws RemoteException {
        return (curent*=a);
    }

    @Override
    public double impartire(double a) throws RemoteException {
        return (curent/=a);
    }

    @Override
    public double factorial() throws RemoteException {
        double fact = 1; // this  will be the result
        for (double i = 1; i <= curent; i++) {
            fact *= i;
        }
        curent=fact;
        return curent;
    }

    @Override
    public double putere(double a) throws RemoteException {
        return (curent = Math.pow(curent, a));
    }

    @Override
    public double radical() throws RemoteException {
        return (curent=Math.sqrt(curent));
    }

    @Override
    public double invers() throws RemoteException {
        return (curent=1/curent);
    }

    @Override
    public double memorysum() throws RemoteException {
        return (memory+=curent); 
    }

    @Override
    public double memorydif() throws RemoteException {
        return (memory-=curent);
    }

    @Override
    public double memoryread() throws RemoteException {
        return memory;
    }

    @Override
    public void memoryset(double a) throws RemoteException {
        memory = a;
    }

    @Override
    public void memoryfree() throws RemoteException {
        memory = 0;
    }

    @Override
    public void free() throws RemoteException {
        curent = 0.0;
    }

    @Override
    public void setcurent(double a) throws RemoteException {
        curent = a;
    }

    @Override
    public double getcurent() throws RemoteException {
        return curent;
    }

}