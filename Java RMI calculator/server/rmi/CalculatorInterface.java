package rmi;

import java.rmi.*;
public interface CalculatorInterface extends Remote {
  public void setcurent(double a) throws RemoteException;  
  public double adunare(double  a) throws RemoteException;
  public double scadere(double a) throws RemoteException;
  public double inmultire(double a) throws RemoteException;
  public double impartire(double a) throws RemoteException;
  public double factorial() throws RemoteException;
  public double putere(double a) throws RemoteException;
  public double radical() throws RemoteException;
  public double invers() throws RemoteException;
  public double memorysum() throws RemoteException;
  public double memorydif() throws RemoteException;
  public double memoryread() throws RemoteException;
  public void memoryset(double a) throws RemoteException;
  public void memoryfree() throws RemoteException;
  public void free() throws RemoteException;  
   public double getcurent() throws RemoteException;
}
