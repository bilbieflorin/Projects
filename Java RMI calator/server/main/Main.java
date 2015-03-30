package main;

import rmi.CalculatorGeneratorImpl;
import java.rmi.*;
import java.rmi.registry.*;

public class Main {
  public static void main(String[] args) throws Exception {
    CalculatorGeneratorImpl calculator = new CalculatorGeneratorImpl();  
    Registry reg = LocateRegistry.createRegistry(1099);
    reg.rebind("Calculator", calculator);
    System.out.println("Serverul s-a legat la registrul RMI");
  }
}
