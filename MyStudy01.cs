using System;

namespace MediatorMS01
{
    public interface IMediator
    {
        void Send(string msg, Colleague colleague);
    }

    public abstract class Colleague
    {
        protected IMediator _mediator;

        protected Colleague(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual void Send(string msg)
        {
            _mediator.Send(msg, this);
        }

        public abstract void Notify(string msg);
    }

    public class Customer : Colleague
    {
        public Customer(IMediator mediator) : base(mediator) { }

        public override void Notify(string msg)
        {
            Console.WriteLine($"Message for customer: {msg}");
        }
    }

    public class Coder : Colleague
    {
        public Coder(IMediator mediator) : base(mediator)
        {
        }

        public override void Notify(string msg)
        {
            Console.WriteLine($"Message for coder: {msg}");
        }
    }

    public class Tester : Colleague
    {
        public Tester(IMediator mediator) : base(mediator)
        {
        }

        public override void Notify(string msg)
        {
            Console.WriteLine($"Message for tester: {msg}");
        }
    }

    public class ManagerMediator : IMediator
    {
        public Colleague customer;
        public Colleague coder;
        public Colleague tester;

        void IMediator.Send(string msg, Colleague colleague)
        {
            if (colleague == customer)
            {
                coder.Notify(msg);
            }
            else if (colleague == coder)
            {
                tester.Notify(msg);
            }
            else if (colleague == tester)
            {
                customer.Notify(msg);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ManagerMediator mediator = new ManagerMediator();

            Colleague customer = new Customer(mediator);
            Colleague coder = new Coder(mediator);
            Colleague tester = new Tester(mediator);

            mediator.customer = customer;
            mediator.coder = coder;
            mediator.tester = tester;

            customer.Send("I wanna get the app");
            coder.Send("It's implemented");
            tester.Send("The app is tested and approved");

            Console.ReadLine();
        }
    }
}