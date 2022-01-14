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

            // if we remove the EndWorkflow() then this would result - Workflow  raised error on step 6 Message: Object reference not set to an instance of an object.|
            // System.NullReferenceException: Object reference not set to an instance of an object.
        }


        public string Id => nameof(EvaluationWorkflow);
        public int Version => 1;
    }
}