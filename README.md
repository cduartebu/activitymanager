# Activity Manager



## Metodología

La metodología escogida para la ejecución del proyecto fue SCRUM, ya que este permite aplicar procesos donde se aplican de manera regular un conjunto de buenas prácticas para trabajar colaborativamente, en equipo, y obtener el mejor resultado posible de un proyecto. 

En Scrum se realizan entregas parciales y regulares del producto final, priorizadas por el beneficio que aportan al receptor del proyecto. Por ello, Scrum está especialmente indicado para proyectos como este, donde el entorno es complejo, donde se necesita obtener resultados pronto, donde los requisitos son cambiantes o poco definidos, donde la innovación, la competitividad, la flexibilidad y la productividad son fundamentales.

Aunque para este ejercicio sólo se tuvieron en cuenta 4 funcionalidades, la metodología está en la capacidad de adoptar nuevos requerimientos y nuevas entregas del mismo.

## Herramienta para SCRUM como metodología ágil

Como un acercamiento inicial a la ejecución del proyecto con la metodología SRUM, se plantan las siguientes fases las cuales permiten el correcto flujo de trabajo, logrando con éxito que el producto final cumpla con las expectativas inicialmente planteadas.

### Identificación de actores

El sistema esta incialmente planteado para ser usado por un actor que en este caso es el Cliente (Product Owner), el cual será el encargado de plantear los requerimientos y aceptar el producto final.

### Identificación de requerimientos (Product Backlog Item)

Desarrollar una funcionalidad que permita crear y modificar proyectos 
Desarrollar una funcionalidad que permita crear y modificar actividades y asociarlas a un proyecto 
Desarrollar una funcionalidad que permita crear y modificar equipo y asociarlas actividades
Desarrollar una funcionalidad que permita crear y modificar integrantes y que se asociarlas actividades

### Identificación de funcionalidades (Historias de usuarios)

* Crear CRUD para proyectos
* Crear CRUD para equipos 
* Crear CRUD para actividades
* Crear CRUD para integrantes

### Identificación de funcionalidad base

Crear información para proyectos, actividades, equipos  

### Identificación de actividades para el desarrollo de la funcionalidad base

* Crear DB-Tablas
* Crear Clase Connection
* Crear Frontend   
* Crear estilo CSS
* Crear JavaScript
* Crear modelo
* Crear Api Rest
* Crear pruebas unitarias
* Ejecutar pruebas
* Pruebas QA

### Número o cantidad de Sprint (Especificar duración de cada uno)

Para el desarrollo del se tiene estimado 4 Sprint cada uno con duración de 7 días.  El primer sprint desarrollo de los proyectos, el segundo sprint actividades, tercer sprint equipos y el cuarto integrantes.

### Cantidad de recursos en términos de perfiles (Equipo de trabajo)

**Desarrollador**  

Para el desarrollo del proyecto se necesitarán 4 desarrolladores estándar que tengan experiencia en el lenguaje de programación .NET.

**Desarrollador experto DBA**

Para el desarrollo del proyecto se necesitará 1 persona experta en base de datos.

**Desarrollador experto en QA**

Para el desarrollo del proyecto se necesitará 2 personas expertas que están evaluando constantemente la calidad del software y aseguren la calidad del mismo.

### HU: Funcionalidad base de historia de usuario. 

**Sprint 1 – Crear CRUD para equipos**

| Prioridad  | Como | Necesito | Para | Criterios de aceptación |
| ---------- | ------------- | ------------- | ------------- |------------- |
| 1  | Crear CRUD para equipos  |  Información de los equipos  | Registrar y  almacenar la información y de los equipos | 1. Registrar información de os equipos 2. Asignar actividades del equipo 3. Visualización los equipos  |

**Sprint 2 – Crear CRUD para proyectos**

Prioridad	Como	Necesito	Para	Criterios de aceptación
1	Crear CRUD para proyectos	Información de los proyectos  	Registrar y almacenar la información de los proyectos 	1. Registrar información del proyectos
				2. Asociar equipos al proyecto 
				2. Visualización de los proyectos registrados
				
**Sprint 3 – Crear CRUD para actividades**

Prioridad	Como	Necesito	Para	Criterios de aceptación
1	Crear CRUD para actividades	Información de actividades de los proyectos 	Registrar y  almacenar la información y asociarla a los proyectos 	1. Registrar información de la actividad
				2. Asociar a un equipo la actividad
				3. Visualización las actividades a un proyectos 


**Sprint 4 – Crear CRUD para integrantes**

Prioridad	Como	Necesito	Para	Criterios de aceptación
1	Crear CRUD para integrantes	Información de los integrantes 	Registrar y  almacenar la información de los integrantes 	1. Registrar información de la integrantes
				2. Asociar los integrantes de los equipos
				3. Visualización las integrantes

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

Otras métricas adicionales que muestran los índices de complejidad ciclomática y acoplamiento.

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

Para incluir el proceso de despliegue continuo y automático, definimos un pipeline para tomar el último build de master y desplegarlo como un servicio en Azure, a continuación, la definición del pipeline.

![](Images/Azure-PipeLine/cd-pipeline-definition.png)

Los últimos despliegues realizados.

![](Images/Azure-PipeLine/cd-pipeline.png)

### Test Plans

#### Test Plan

#### Load Test

Con Azure DevOps definimos una prueba de carga usando el servicio de consulta de proyectos, para esta prueba se usaron 25 usuarios, 2625 http requests en 2min y los resultados se presentan en la siguiente gráfica.

![](Images/LoadTest/loadtest.png)

## Authors

* **Carlos Avella** 
* **Carlos Duarte** 
* **Christian Rojas** 
* **Edgar Vásquez** 
