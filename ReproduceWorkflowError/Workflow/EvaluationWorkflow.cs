using ReproduceWorkflowError.Workflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow
{
    public class EvaluationWorkflow : IWorkflow<EvaluationContext>
    {
        private static void DoNothing() => ExecutionResult.Next();

        public void Build(IWorkflowBuilder<EvaluationContext> builder)
        {
            builder.StartWith<EvaluateInput>()
                .Input((step, context) => { step.InputNumber = context.InputNumber; })
                .When(_ => EvalResult.Even)
                .Do(then => then
                    .StartWith<IncrementNumber>()
                    .Input((step, context) => { step.InputNumber = context.InputNumber; }))
                .When(_ => EvalResult.Odd)
                .Do(then => then
                    .StartWith<DecrementNumber>()
                    .Input((step, context) => { step.InputNumber = context.InputNumber; }))
                .When(_ => EvalResult.None)
                .Do(then => DoNothing())
                .EndWorkflow();


        }


        public string Id => nameof(EvaluationWorkflow);
        public int Version => 1;
    }
}