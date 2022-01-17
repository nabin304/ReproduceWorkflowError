using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow.Steps;

public class ProcessOddNumber : StepBodyAsync
{
    public int InputNumber { private get; set; }


    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Yield();

        // process the number.. do anything
        InputNumber--;

        return ExecutionResult.Next();
    }
}