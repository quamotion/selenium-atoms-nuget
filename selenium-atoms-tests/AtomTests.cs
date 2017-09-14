using System;
using System.Collections.Generic;
using Xunit;

namespace Selenium.Atoms.Tests
{
    public class AtomTests
    {
        public static IEnumerable<object[]> GetAtomCombinations()
        {
            foreach (var atom in Enum.GetValues(typeof(AtomType)))
            {
                foreach (var target in Enum.GetValues(typeof(AtomTarget)))
                {
                    yield return new object[] { (AtomType)atom, (AtomTarget)target };
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetAtomCombinations))]
        public void GetInjectableAtomTest(AtomType atom, AtomTarget target)
        {
            Assert.NotNull(Atom.GetInjectableAtom(atom, target));
        }

        [Fact]
        public void GetInvalidInjectableAtomTest()
        {
            Assert.Throws<AtomNotFoundException>(Atom.GetInjectableAtom("abc", AtomTarget.Android));
        }
    }
}
