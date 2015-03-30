package rmi;

import java.rmi.*;

public interface CalculatorGeneratorInterface extends Remote {
  CalculatorInterface server_propriu() throws Exception;
}
