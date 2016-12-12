project(TexturedBall)

cmake_minimum_required(VERSION 2.8)
add_definitions(-std=c++14 -g -Wall -Wextra -O3)

find_package(Boost COMPONENTS system filesystem REQUIRED)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})
find_package(OpenCV REQUIRED)
include_directories(${OpenCV_INCLUDE_DIRS})


add_executable(${PROJECT_NAME} main.cpp)

target_link_libraries(${PROJECT_NAME} ${OpenCV_LIBS})
target_link_libraries(${PROJECT_NAME} ${Boost_SYSTEM_LIBRARY})
target_link_libraries(${PROJECT_NAME} ${Boost_FILESYSTEM_LIBRARY})

