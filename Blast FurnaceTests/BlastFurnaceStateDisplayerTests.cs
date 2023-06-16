using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewLab10;
using ViewLab10.Model;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ViewLab10Tests;

[TestClass]
public class BlastFurnaceStateDisplayerTests
{
    BlastFurnace furnace;
    BlastFurnaceStateDisplayer stateDisplayer;
    BlastFurnaceSmeltingTimer smeltingTimer;
     BlastFurnaceThermometer termometer;
    [TestInitialize]
    public void Init()
    {
       
        furnace = new BlastFurnace();
        stateDisplayer = new BlastFurnaceStateDisplayer(furnace);
        termometer=new BlastFurnaceThermometer(furnace);
        furnace.Max_temperature = 2000;
         smeltingTimer =new BlastFurnaceSmeltingTimer(furnace);
    }
     [TestMethod]
    public void BlastFurnaceStateDisplayerTestsFromHeatingToMaintaining()
    {
        furnace.Forward();
        furnace.Forward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Maintaining;
        Assert.AreEqual(expected, actual); 
    } 
    [TestMethod]
    public void BlastFurnaceStateDisplayerTestsFromOfflineToHeating()
    {
        furnace.Forward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Heating;
        Assert.AreEqual(expected, actual); 
    }
      [TestMethod]
    public void BlastFurnaceStateDisplayerTestsFromHeatingToToCooling()
    {
        furnace.Forward();
        furnace.Backward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Cooling;
        Assert.AreEqual(expected, actual); 
    }  
    [TestMethod]
    public void BlastFurnaceStateDisplayerTestsFromMaintainingToToCooling()
    {
        furnace.Forward();
        furnace.Forward();
         furnace.Backward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Cooling;
        Assert.AreEqual(expected, actual); 
    }  

     [TestMethod]
    public void BlastFurnaceStateDisplayerTestsFromCoolingToToHeating()
    {
        furnace.Forward();
        furnace.Forward();
         furnace.Backward();
         furnace.Forward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Heating;
        Assert.AreEqual(expected, actual); 
    }  
    [TestMethod]
     public void BlastFurnaceStateDisplayerTestsFromCoolingToOffline()
    {
       
        furnace.Backward();
        int actual = stateDisplayer.CurrentState;
        int expected = (int)FurnaceStates.Offline;
        Assert.AreEqual(expected, actual); 
    } 
    [TestMethod]
    public void BlastFurnaceSmeltingTimerGetWorkTimeTestWithOneForward()
    {
        Stopwatch stopwatch = new Stopwatch();
         stopwatch.Start(); 
         furnace.Forward();
         Thread.Sleep(10000);
        TimeSpan ts = stopwatch.Elapsed;
         string expected= String.Format("{0:00}:{1:00}", 
         ts.Minutes, ts.Seconds);
         string actual=smeltingTimer.GetWorkTime();
          Assert.AreEqual(expected, actual); 

    }  
       [TestMethod]
     public void BlastFurnaceSmeltingTimerGetWorkTimeTestWithBackward()
     {
        Stopwatch stopwatch = new Stopwatch();
         stopwatch.Start(); 
         furnace.Forward();
         Thread.Sleep(10000);
        furnace.Backward();
         Thread.Sleep(1000);
        TimeSpan ts = stopwatch.Elapsed;
         string expected= String.Format("{0:00}:{1:00}", 
         ts.Minutes, ts.Seconds);
      string actual=smeltingTimer.GetWorkTime();
          Assert.AreEqual(expected, actual); 

     }   
  [TestMethod]
     public void BlastFurnaceThermometerTest()
     {
        furnace.Forward();
        furnace.Max_temperature = 200;
        while (stateDisplayer.CurrentState==(int)FurnaceStates.Heating)
        {
            Thread.Sleep(100);
        }
         Assert.AreEqual(furnace.Max_temperature,termometer.Furnace_temperature); 

     }    


}



