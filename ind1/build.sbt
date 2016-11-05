lazy val root = (project in file(".")).
  settings(
    name := "Individual",
    version := "1.0",
    scalaVersion := "2.11.6"
  )

libraryDependencies += "org.scala-lang.modules" %% "scala-swing" % "1.0.1"
