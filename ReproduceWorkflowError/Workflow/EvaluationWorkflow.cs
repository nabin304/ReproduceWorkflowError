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
            builder
                .StartWith<EvaluateInput>()
                    .Input((step, context) => { step.InputNumber = context.InputNumber; })
                .When(_ => EvalResult.Even)
                     .Do(then => then
                        .StartWith<ProcessEvenNumber>()
                            .Input((step, context) => { step.InputNumber = context.InputNumber; }))
                .When(_ => EvalResult.Odd)
                    .Do(then => then
                        .StartWith<ProcessOddNumber>()
                            .Input((step, context) => { step.InputNumber = context.InputNumber; }))
                .When(_ => EvalResult.None)
                    .Do(then => DoNothing());

            // On executing the last statement (pass a negative number as an Step input): results an error on the log message:  Workflow  raised error on step 6 Message: Object reference not set to an instance of an object.|
            // System.NullReferenceException: Object reference not set to an instance of an object.
        }


        public string Id => nameof(EvaluationWorkflow);
        public int Version => 1;
    }
}