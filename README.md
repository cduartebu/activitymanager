# Activity Manager



## Metodología

## Herramienta para SCRUM como metodología ágil

## Unit Test

Para la inclusión de pruebas de unidad se usó la extensión de Visual Studio - UnitTest, el IDE de desarrollo provee la capacidad de ejecutar las pruebas, ver resultados, ver cobertura de código y la funcionalidad "Live Unit Testing" que advierte al momento de hacer cambios en el código si alguna de las pruebas relacionadas ha fallado, por lo que no tenemos que esperar hasta acabar de escribir el código y ejecutar pruebas para conocer el resultado de ellas haciendo más rápido el desarrollo.

Para evitar hacer llamados a otras clases y/o componentes externos como la base de datos, se usó RhinoMocks para generar Mocks de las clases que se depende y poder inyectar el comportamiento simulado en tiempo de ejecución.



### TDD

Usamos TDD como metodología de desarrollo, puesto que al orientar el desarrollo basado en las pruebas hace que se eviten refactors y cambios futuros que pueden ser detectados con anterioridad. Para estos se definieron los escenarios de prueba basados en los criterios de aceptación de cada historia de usuario y tomando este insumo se definieron los casos de pruebas unitarias.

### CodeCoverage

Con ayuda de la versión Enterprise de Visual Studio medimos la cobertura de las pruebas unitarias, los resultados se muestran en la siguiente gráfica.

![](Images/UnitTest/code-coverage.png)

## Code Quality (SonarQube & SonarCloud)

Con el fin de tener métricas de nuestro código y tener una idea de que tan mantenible es. Incluimos un análisis de código estático usando SonarQube, los informes de errores, defectos y vulnerabilidades encontradas en el proyecto se muestran a continuación.

### SonarCloud - OverView
Estos son los resultados del último análisis de código.

![](Images/SonarQube/overview.png)

### SonarCloud - Issues

Algunos de los posibles problemas de código que la herramienta detectó por solucionar.

![](Images/SonarQube/issues.png)

### SonarCloud - Measures

La gráfica muestra la confiabilidad del código y el tiempo que tomaría solucionar la deuda técnica.

![](Images/SonarQube/messures.png)

### SonarCloud - Code

Tabla de resumen de más métricas del código por componentes.

![](Images/SonarQube/code.png)

### Visual Studio - métricas

Otras métricas adicionales que muestran los indices de complejidad ciclomática y acoplamiento.

![](Images/SonarQube/code-metrics.png)

## Azure DevOps

Usamos Azure DevOps como herramienta principal para el desarrollo del proyecto, puesto que soporta el manejo de épicas, historias de usuario, backlog items, dashboards, sprints, plan de pruebas y todo el soporte para DevOps.

### Board 

### Pipelines
Se definieron dos Pipelines uno para CI y para CD. 

#### Builds
Para el desarrollo del CI, se ha definió un build con un trigger sobre la rama master.
![](Images/Azure-PipeLine/pipeline.png)

Cómo se muestra en la definición del build, se recupera la última versión del branch, luego se restauran los Nuget packages, se compila y se genera un zip de publicación en modo 'Release' seguido de la ejecución de pruebas unitarias y por último se almacenan los artefactos generados del build.

![](Images/Azure-PipeLine/pipeline-definition.png)

Después de la ejecución de las pruebas unitarias los resultados son publicados en cada build.

![](Images/Azure-PipeLine/pipeline-master.png)
![](Images/Azure-PipeLine/pipeline-unittest.png)

#### Releases

Para incluir el proceso de despligue continuo y automático, definimos un pipeline para tomar el último build de master y desplegarlo como un servicio en Azure, a continuación, la definición del pipeline.

![](Images/Azure-PipeLine/cd-pipeline-definition.png)

Los últimos despliegues realizados.

![](Images/Azure-PipeLine/cd-pipeline.png)

### Test Plans

#### Test Plan

#### Load Test
