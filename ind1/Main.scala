import swing.{Panel, MainFrame, SimpleSwingApplication}
import java.awt.{Color, Graphics2D, Dimension}

class DataPanel(data: Array[Array[Color]]) extends Panel {

  override def paintComponent(g: Graphics2D) {
    val dx = g.getClipBounds.width.toFloat  / data.length
    val dy = g.getClipBounds.height.toFloat / data.map(_.length).max
    for {
      x <- 0 until data.length
      y <- 0 until data(x).length
      x1 = (x * dx).toInt
      y1 = (y * dy).toInt
      x2 = ((x + 1) * dx).toInt
      y2 = ((y + 1) * dy).toInt
    } {
      data(x)(y) match {
        case c: Color => g.setColor(c)
        case _ => g.setColor(Color.WHITE)
      }
      g.fillRect(x1, y1, x2 - x1, y2 - y1)
    }
  }
}



object Draw extends SimpleSwingApplication {

  var edges = List(
    Edge((0, 0), (0, 2)),
    Edge((0, 2), (2, 2)),
    Edge((2, 2), (1, 1)),
    Edge((1, 1), (2, 0)),
    Edge((2, 0), (0, 0)))

  var polygon = new Polygon(edges)

  var point1 = (1.0, 1.0)
  var point2 = (5.0, 5.0)
  var point3 = (0.5, 0.5)
  val data = Array.ofDim[Color](25, 25)

  // plot some points
  data(0)(0) = Color.BLACK
  data(4)(4) = Color.RED
  data(0)(4) = Color.GREEN
  data(4)(0) = Color.BLUE

  // draw a circle 
  import math._
  {
    for {
      t <- Range.Double(0, 2 * Pi, Pi / 60)
      x = 12.5 + 10 * cos(t)
      y = 12.5 + 10 * sin(t)
      c = new Color(0.5f, 0f, (t / 2 / Pi).toFloat)
    } data(x.toInt)(y.toInt) = c
  }

  def top = new MainFrame {
    contents = new DataPanel(data) {
      preferredSize = new Dimension(300, 300)
    }
  }
}




// function BowyerWatson (pointList)
//       // pointList is a set of coordinates defining the points to be triangulated
//       triangulation := empty triangle mesh data structure
//       add super-triangle to triangulation // must be large enough to completely contain all the points in pointList
//       for each point in pointList do // add all the points one at a time to the triangulation
//          badTriangles := empty set
//          for each triangle in triangulation do // first find all the triangles that are no longer valid due to the insertion
//             if point is inside circumcircle of triangle
//                add triangle to badTriangles
//          polygon := empty set
//          for each triangle in badTriangles do // find the boundary of the polygonal hole
//             for each edge in triangle do
//                if edge is not shared by any other triangles in badTriangles
//                   add edge to polygon
//          for each triangle in badTriangles do // remove them from the data structure
//             remove triangle from triangulation
//          for each edge in polygon do // re-triangulate the polygonal hole
//             newTri := form a triangle from edge to point
//             add newTri to triangulation
//       for each triangle in triangulation // done inserting points, now clean up
//          if triangle contains a vertex from original super-triangle
//             remove triangle from triangulation
//       return triangulation



// import java.awt.image.BufferedImage
// import java.awt.{Graphics2D,Color,Font,BasicStroke}
// import java.awt.geom._
// object Main extends App { 
//     // Size of image
//     val size = (500, 500)

//     // create an image
//     val canvas = new BufferedImage(size._1, size._2, BufferedImage.TYPE_INT_RGB)

//     // get Graphics2D for the image
//     val g = canvas.createGraphics()

//     // clear background
//     g.setColor(Color.WHITE)
//     g.fillRect(0, 0, canvas.getWidth, canvas.getHeight)

//     // enable anti-aliased rendering (prettier lines and circles)
//     // Comment it out to see what this does!
//     g.setRenderingHint(java.awt.RenderingHints.KEY_ANTIALIASING, 
//                java.awt.RenderingHints.VALUE_ANTIALIAS_ON)

//     // draw two filled circles
//     g.setColor(Color.RED)
//     g.fill(new Ellipse2D.Double(30.0, 30.0, 40.0, 40.0))
//     g.fill(new Ellipse2D.Double(230.0, 380.0, 40.0, 40.0))

//     // draw an unfilled circle with a pen of width 3
//     g.setColor(Color.MAGENTA)
//     g.setStroke(new BasicStroke(3f))
//     g.draw(new Ellipse2D.Double(400.0, 35.0, 30.0, 30.0))

//     // draw a filled and an unfilled Rectangle
//     g.setColor(Color.CYAN)
//     g.fill(new Rectangle2D.Double(20.0, 400.0, 50.0, 20.0))
//     g.draw(new Rectangle2D.Double(400.0, 400.0, 50.0, 20.0))

//     // draw a line
//     g.setStroke(new BasicStroke())  // reset to default
//     g.setColor(new Color(0, 0, 255)) // same as Color.BLUE
//     g.draw(new Line2D.Double(50.0, 50.0, 250.0, 400.0))

//     // draw some text
//     g.setColor(new Color(0, 128, 0)) // a darker green
//     g.setFont(new Font("Batang", Font.PLAIN, 20))
//     g.drawString("Hello World!", 155, 225)
//     g.drawString("안녕 하세요", 175, 245)

//     // done with drawing
//     g.dispose()

//     // write image to a file
//     javax.imageio.ImageIO.write(canvas, "png", new java.io.File("drawing.png"))
// } 
