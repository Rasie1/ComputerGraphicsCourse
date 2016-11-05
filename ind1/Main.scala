import swing.{Panel, MainFrame, SimpleSwingApplication}
import java.awt.{Color, Graphics2D, Dimension, Font, BasicStroke}
import java.awt.image.BufferedImage
import java.awt.geom._



object Config {
    final val WindowSize = (512, 512)
}

class DataPanel extends Panel {

  def drawEdge(g: Graphics2D, e: Edge) {
    g.draw(new Line2D.Double(e.a._1, e.a._2, 
                             e.b._1, e.b._2))
  }

  def drawPolygon(g: Graphics2D, p: Polygon) {

    g.setStroke(new BasicStroke())  // reset to default
    g.setColor(new Color(0, 0, 255)) // same as Color.BLUE
    p.edges map (e => drawEdge(g, e))
  }


  override def paintComponent(g: Graphics2D) {
    // val dx = g.getClipBounds.width.toFloat  / data.length
    // val dy = g.getClipBounds.height.toFloat / data.map(_.length).max
    // for {
    //   x <- 0 until data.length
    //   y <- 0 until data(x).length
    //   x1 = (x * dx).toInt
    //   y1 = (y * dy).toInt
    //   x2 = ((x + 1) * dx).toInt
    //   y2 = ((y + 1) * dy).toInt
    // } {
    //   data(x)(y) match {
    //     case c: Color => g.setColor(c)
    //     case _ => g.setColor(Color.WHITE)
    //   }
    //   g.fillRect(x1, y1, x2 - x1, y2 - y1)
    // }
    val size = Config.WindowSize

    val canvas = new BufferedImage(size._1, size._2, BufferedImage.TYPE_INT_RGB)

    g.setColor(Color.WHITE)
    g.fillRect(0, 0, canvas.getWidth, canvas.getHeight)

    val offset = 128

    var edges = List(
      Edge((offset + 0, offset + 0), (offset + 0, offset + 200)),
      Edge((offset + 0, offset + 200), (offset + 200, offset + 200)),
      Edge((offset + 200, offset + 200), (offset + 100, offset + 100)),
      Edge((offset + 100, offset + 100), (offset + 200, offset + 0)),
      Edge((offset + 200, offset + 0), (offset + 0, offset + 0)))

    var polygon = new Polygon(edges)

    drawPolygon(g, polygon)

    g.dispose()
  }
}



object Draw extends SimpleSwingApplication {


  def top = new MainFrame {
    contents = new DataPanel {
      preferredSize = new Dimension(Config.WindowSize._1, Config.WindowSize._2)
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
