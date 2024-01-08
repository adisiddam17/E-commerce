namespace HRApp.Entities;
public class Employee{
    public int Id{get;set;}  
    public string? Name{get;set;}
    public double BasicSal{get;set;}
    public double DailyAllo{get;set;}
    public double Tax{get;set;}
    public int NoOfWorkingDays{get;set;}

    public Employee()
    {
        
    }
    public Employee(int id,string name,double bsal,double allo,double tax,int nowd)
    {
        Id=id;
        Name=name;
        BasicSal=bsal;
        DailyAllo=allo;
        Tax=tax;
        NoOfWorkingDays=nowd;
    }

    public override string ToString()
    {
        return this.Id+"  "+this.Name+"  "+this.BasicSal+"  "+this.DailyAllo+"  "+this.Tax+"  "+this.NoOfWorkingDays+"  ";
    }

}