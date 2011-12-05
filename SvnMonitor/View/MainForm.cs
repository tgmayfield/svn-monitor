// Stack empty.
//    at System.Collections.Generic.Stack`1.Peek()
//    at ..(MethodReference method)
//    at ..OnCall(Instruction instruction)
//    at ..(Instruction instruction, IInstructionVisitor visitor)
//    at Telerik.JustDecompiler.Decompiler.StatementDecompiler.(Instruction instruction)
//    at Telerik.JustDecompiler.Decompiler.StatementDecompiler.(InstructionBlock block)
//    at Telerik.JustDecompiler.Decompiler.StatementDecompiler.(InstructionBlock block)
//    at Telerik.JustDecompiler.Decompiler.StatementDecompiler.()
//    at Telerik.JustDecompiler.Decompiler.StatementDecompiler.Process(DecompilationContext context, BlockStatement body)
//    at Telerik.JustDecompiler.Decompiler.DecompilationPipeline.Run(MethodBody body, ILanguage language)
//    at Telerik.JustDecompiler.Decompiler.Extensions.(DecompilationPipeline pipeline, ILanguage language, MethodBody body)
//    at Telerik.JustDecompiler.Languages.BaseImperativeLanguageWriter.Write(MethodDefinition method)
//    at Telerik.JustDecompiler.Languages.BaseLanguageWriter.(IMemberDefinition member, Boolean isFirstMember)
//    at Telerik.JustDecompiler.Languages.BaseLanguageWriter.(TypeDefinition type, Func`3 writeMember, Boolean writeNewLine, Boolean showCompilerGeneratedMembers)
//    at Telerik.JustDecompiler.Languages.BaseLanguageWriter.Write(TypeDefinition type, Func`3 writeMember, Boolean writeNewLine, Boolean showCompilerGeneratedMembers)
//    at Telerik.JustDecompiler.Languages.BaseLanguageWriter.WriteType(TypeDefinition type, Boolean showCompilerGeneratedMembers)
//    at Telerik.JustDecompiler.Languages.NamespaceImperativeLanguageWriter.WriteTypeAndNamespaces(TypeDefinition type, Boolean showCompilerGeneratedMembers)
//    at JustDecompile.Tools.MSBuildProjectBuilder.MSBuildProjectBuilder.BuildProject(CancellationToken cancellationToken) in c:\Builds\126\Behemoth\JustDecompile Production Build\Sources\Tools\MSBuildProjectCreator\MSBuildProjectBuilder.cs:line 104
