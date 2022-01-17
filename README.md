## Bug
Let's suppose we have a workflow containing a `When ` statement. This would result in an error message in the log file : 

``` csharp
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
```


```
  [ERR]|WorkflowCore.Services.WorkflowExecutor|Workflow b61539e5-8cc6-4710-92f8-087307f8f059 raised error on step 6 Message: Object reference not set to an instance of an object.|
System.NullReferenceException: Object reference not set to an instance of an object.
   at WorkflowCore.Services.ExecutionPointerFactory.BuildChildPointer(WorkflowDefinition def, ExecutionPointer pointer, Int32 childDefinitionId, Object branch)
   at WorkflowCore.Services.ExecutionResultProcessor.ProcessExecutionResult(WorkflowInstance workflow, WorkflowDefinition def, ExecutionPointer pointer, WorkflowStep step, ExecutionResult result, WorkflowExecutorResult workflowResult)
   at WorkflowCore.Services.WorkflowExecutor.ExecuteStep(WorkflowInstance workflow, WorkflowStep step, ExecutionPointer pointer, WorkflowExecutorResult wfResult, WorkflowDefinition def, CancellationToken cancellationToken)
   at WorkflowCore.Services.WorkflowExecutor.Execute(WorkflowInstance workflow, CancellationToken cancellationToken)
```
**To Reproduce**
[Here I have created a demo Web API to reproduce this issue ](https://github.com/nabin304/ReproduceWorkflowError)

It offers an endpoint that takes integer numbers as an input. If the number is positive `ProcessEvenNumber` workflow step gets executed, and if is negative then `ProcessOddNumber` step will be executed, and if it is a negative one then does nothing.

So if I give it a negative number to execute the last condition of the When statement, then results in above mentined error log message.

>Note: To resolve this issue once can Add the Endworkflow statement then the error message will be gone. Unfortunately, I did not see any documentation about whether we really need to end the workflow or not.
