using Automatonymous;
using Core.Contracts.Consumers.Categories;
using MassTransit;

namespace Infrastructure.SagaPattern
{
    /*public class CategoryStateMachine : MassTransitStateMachine<CategoryState>
    {
        public CategoryStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CategoryCreated, x =>
            {
                x.CorrelateById(context => context.Message.CategoryId);
                x.SelectId(context => context.Message.CategoryId);
            });

            Initially(
                When(CategoryCreated)
                    .Then(context =>
                    {
                        var category = context.Data;

                        // Perform initial state transition or any other actions
                        // based on the CategoryCreated event
                    })
                    .TransitionTo(Initial)
            );
        }

        public State Initial { get; private set; }
        public Event<CategoryCreated> CategoryCreated { get; private set; }
    }*/
}
