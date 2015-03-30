
package rmi;

import java.rmi.*; 
import java.rmi.server.*;

public class CalculatorGeneratorImpl extends UnicastRemoteObject 
                       implements CalculatorGeneratorInterface {
  static int contor;
  public CalculatorGeneratorImpl() throws RemoteException {}
	
  @Override
  public CalculatorInterface server_propriu() throws  Exception {
    return new CalculatorImpl();
  }
}