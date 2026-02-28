using System;
using System.Collections.Generic;

public abstract class Subscription
{
    public abstract double MonthlyFee { get; }
    public abstract int MinimumPeriod { get; } 
    public abstract List<string> Channels { get; }

    public void PrintDetails()
    {
        Console.WriteLine($"Тип: {this.GetType().Name}");
        Console.WriteLine($"Ціна: {MonthlyFee} грн/міс");
        Console.WriteLine($"Мін. період: {MinimumPeriod} міс.");
        Console.WriteLine($"Канали: {string.Join(", ", Channels)}\n");
    }
}

public class DomesticSubscription : Subscription
{
    public override double MonthlyFee => 150.0;
    public override int MinimumPeriod => 1;
    public override List<string> Channels => new List<string> { "Новини", "Кіно", "Спорт" };
}

public class EducationalSubscription : Subscription
{
    public override double MonthlyFee => 100.0;
    public override int MinimumPeriod => 3;
    public override List<string> Channels => new List<string> { "Discovery", "National Geographic", "History" };
}

public class PremiumSubscription : Subscription
{
    public override double MonthlyFee => 400.0;
    public override int MinimumPeriod => 12;
    public override List<string> Channels => new List<string> { "4K Movies", "All Sports", "HBO", "Netflix Integration" };
}

public abstract class PurchaseChannel
{
    public abstract Subscription CreateSubscription();

    public void ProcessPurchase()
    {
        var subscription = CreateSubscription();
        Console.WriteLine($"--- Обробка замовлення через {this.GetType().Name} ---");
        subscription.PrintDetails();
    }
}

public class WebSite : PurchaseChannel
{
    public override Subscription CreateSubscription() => new DomesticSubscription();
}

public class MobileApp : PurchaseChannel
{
    public override Subscription CreateSubscription() => new EducationalSubscription();
}

public class ManagerCall : PurchaseChannel
{
    public override Subscription CreateSubscription() => new PremiumSubscription();
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PurchaseChannel[] channels = { new WebSite(), new MobileApp(), new ManagerCall() };

        foreach (var channel in channels)
        {
            channel.ProcessPurchase();
        }
    }
}