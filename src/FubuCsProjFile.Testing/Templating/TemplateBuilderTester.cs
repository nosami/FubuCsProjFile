﻿using FubuCsProjFile.Templating;
using FubuCsProjFile.Templating.Planning;
using FubuCsProjFile.Templating.Runtime;
using NUnit.Framework;
using FubuTestingSupport;
using System.Linq;
using FubuCore;

namespace FubuCsProjFile.Testing.Templating
{
    [TestFixture]
    public class TemplateBuilderTester
    {


        [Test]
        public void description_txt_is_ignored()
        {
            var mother = new DataMother("empty");
            mother.ToPath("description.txt").WriteEmpty();

            var plan = mother.BuildSolutionPlan();
            plan.FileIsUnhandled("description.txt").ShouldBeFalse();

            plan.Steps.OfType<CopyFileToSolution>().Any().ShouldBeFalse();
        }


        [Test]
        public void inputs_txt_is_ignored()
        {
            var mother = new DataMother("empty");
            mother.ToPath("inputs.txt").WriteEmpty();

            var plan = mother.BuildSolutionPlan();
            plan.FileIsUnhandled("inputs.txt").ShouldBeFalse();

            plan.Steps.OfType<CopyFileToSolution>().Any().ShouldBeFalse();
        }

        [Test]
        public void instructions_txt_is_ignored()
        {
            var mother = new DataMother("empty");
            mother.ToPath(TemplatePlan.InstructionsFile).WriteEmpty();

            var plan = mother.BuildSolutionPlan();
            plan.FileIsUnhandled(TemplatePlan.InstructionsFile).ShouldBeFalse();

            plan.Steps.OfType<CopyFileToSolution>().Any().ShouldBeFalse();
        }

        [Test]
        public void other_files_are_copied()
        {
            var mother = new DataMother("copied", false);
            mother.ToPath("foo.txt").WriteEmpty();
            mother.ToPath("bar.txt").WriteEmpty();
            mother.ToPath("deep","nested","topic.txt").WriteEmpty();

            var plan = mother.BuildSolutionPlan();
            plan.Steps.OfType<CopyFileToSolution>().ShouldHaveTheSameElementsAs(
                new CopyFileToSolution("bar.txt", "copied".AppendPath("bar.txt")),
                new CopyFileToSolution("foo.txt", "copied".AppendPath("foo.txt")),
                new CopyFileToSolution("deep/nested/topic.txt", "copied".AppendPath("deep","nested","topic.txt"))
                
                
                
                
                );
        }




    }
}