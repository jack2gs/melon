using System.Threading.Tasks;

namespace XunitExtensions
{
    public abstract class Specification
    {
        protected virtual void Because() { }

        protected virtual void DestroyContext() { }

        protected virtual void EstablishContext() { }

        internal void OnFinish()
        {
            DestroyContext();
        }

        internal void OnStart()
        {
            EstablishContext();
            Because();
        }
        
        protected virtual Task BecauseAsync() => Task.CompletedTask;

        protected virtual Task DestroyContextAsync() => Task.CompletedTask;

        protected virtual Task EstablishContextAsync() => Task.CompletedTask;

        internal async Task OnFinishAsync()
        {
            DestroyContext();
            await DestroyContextAsync();
        }

        internal async Task OnStartAsync()
        {
            await EstablishContextAsync();
            EstablishContext(); // establish context should always happens before calling because
            await BecauseAsync();
            Because();
        }
    }
}