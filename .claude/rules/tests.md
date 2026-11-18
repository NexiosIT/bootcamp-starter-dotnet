---
paths:
  - "**/*.Tests/**/*.cs"
---
- xUnit + FluentAssertions, naam: `Method_Scenario_Expected`
- Arrange / Act / Assert, gescheiden door lege lijn
- Mockito (Java) / NSubstitute (.NET) voor dependencies zoals services en repositories, geen echte DB
