#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <iostream>
#include <cmath>
#include <vector>
using namespace cv;
using namespace std;

Mat renderTarget;
Mat texture;
vector<Point> textureCoords;

void setPixel(const Vec3b& color, Point pos)
{
    renderTarget.at<Vec3b>(pos) = color;
}

Vec3b getTexturePixelValue(Point pos)
{
    return texture.at<Vec3b>(pos);
}

double mapCoordinate(double i1, 
                     double i2, 
                     double w1, 
                     double w2, 
                     double p) 
{
    return ((p - i1) / (i2 - i1)) * (w2 - w1) + w1;
}

double pi = 3.1415;
double phi0 = 0.0;
double phi1 = pi;
double theta0 = 0.0;
double theta1 = 2.0 * pi;
double radius = 50;

double userRotationX =  1.5;
double userRotationY = -2.5;

void rotX(double angle, 
          double& y, 
          double& z)
{
     double y1 = y * cos(angle) - z * sin(angle);
     double z1 = y * sin(angle) + z * cos(angle);
     y = y1;
     z = z1;
}
void rotY(double angle, 
          double& x, 
          double& z)
{
     double x1 = x * cos(angle) - z * sin(angle);
     double z1 = x * sin(angle) + z * cos(angle);
     x = x1;
     z = z1;
}
void rotZ(double angle, 
          double& x, 
          double& y)
{
     double x1 = x * cos(angle) - y * sin(angle);
     double y1 = x * sin(angle) + y * cos(angle);
     x = x1;
     y = y1;
}

void update()
{
    for (int i = 0; i < renderTarget.cols; i++)
        for (int j = 0; j < renderTarget.rows; j++)
            setPixel(Vec3b(0, 0, 0), Point(i, j));

    for (int i = 0; i < renderTarget.cols; i++)
    {
        for (int j = 0; j < renderTarget.rows; j++)
        {
            double theta = mapCoordinate(0.0, 
                                         renderTarget.cols - 1, 
                                         theta1, 
                                         theta0, 
                                         i);
            double phi = mapCoordinate(0.0, 
                                       renderTarget.rows - 1,
                                       phi0, 
                                       phi1, 
                                       j);
            double x = radius * sin(phi) * cos(theta);
            double y = radius * sin(phi) * sin(theta);
            double z = radius * cos(phi);

            rotX(userRotationX, y, z);
            rotY(userRotationY, x, z);

            if (z > 0)
            {
                auto textureCoords = Point(i, j);
                auto color = getTexturePixelValue(textureCoords);
                auto targetCoords = Point((int)x + radius,
                                          (int)y + radius);
                setPixel(color, targetCoords);
            }


        }
    }
    imshow("Display window", renderTarget);

}

int main(int argc, char** argv)
{
    if (argc != 2)
    {
        cout << "Usage: display_image ImageToLoadAndDisplay" << endl;
        return -1;
    }

    renderTarget = imread(argv[1], CV_LOAD_IMAGE_COLOR);   
    texture      = imread(argv[1], CV_LOAD_IMAGE_COLOR); 

    if (!renderTarget.data)
    {
        cout << "Could not open or find the image" << std::endl;
        return -1;
    }

    namedWindow("Display window", WINDOW_AUTOSIZE);  

    while (true)
    {
        update();

        auto code = (waitKey() & 0xEFFFFF);
        auto esc = code == 27;
        auto w = code == 'w';
        auto a = code == 'a';
        auto d = code == 'd';
        auto s = code == 's';
        auto q = code == 'q';
        auto e = code == 'e';
        if (esc)
            break;
        if (w)
            userRotationX += 0.5;
        if (s)
            userRotationX -= 0.5;
        if (a)
            userRotationY -= 0.5;
        if (d)
            userRotationY += 0.5;
        if (q)
            radius -= 10;
        if (e)
            radius += 10;
        if (radius * 2 > renderTarget.cols ||
            radius * 2 > renderTarget.rows)
            radius -= 10;
        if (radius < 0)
            radius = 1;
    }

    return 0;
}
