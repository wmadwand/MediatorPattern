using System;

namespace Recall
{
    public interface IMediator
    {
        void Send(string msg, Colleague colleague);
    }

    public abstract class Colleague
    {
        private IMediator _mediator;

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
        public Customer(IMediator mediator) : base(mediator)
        {
        }

        public override void Notify(string msg)
        {
            Console.WriteLine($"For Customer:{msg}");
        }
    }

    public class Coder : Colleague
    {
        public Coder(IMediator mediator) : base(mediator)
        {
        }

        public override void Notify(string msg)
        {
            Console.WriteLine($"For Coder:{msg}");
        }
    }

    public class Tester : Colleague
    {
        public Tester(IMediator mediator) : base(mediator)
        {
        }

        public override void Notify(string msg)
        {
            Console.WriteLine($"For Tester:{msg}");
        }
    }

    public class Manager : IMediator
    {
        public Colleague _customer, _coder, _tester;

        void IMediator.Send(string msg, Colleague colleague)
        {
            if (colleague == _customer)
            {
                _coder.Notify(msg);

            }
            else if (colleague == _coder)
            {
                _tester.Notify(msg);
            }
            else if (colleague == _tester)
            {
                _customer.Notify(msg);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Manager mediator = new Manager();

            Colleague coder = new Coder(mediator);
            Colleague customer = new Customer(mediator);
            Colleague tester = new Tester(mediator);

            mediator._coder = coder;
            mediator._customer = customer;
            mediator._tester = tester;

            customer.Send("I need app");
            coder.Send("App is done");
            tester.Send("App is finished");
        }
    }
}
