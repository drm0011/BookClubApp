pipeline {
    agent any
    environment {
        DOCKER_USERNAME = credentials('docker-username') // Store your DockerHub username in Jenkins credentials
        DOCKER_PASSWORD = credentials('docker-password') // Store your DockerHub password in Jenkins credentials
        SONAR_TOKEN = credentials('sonar-token') // Store your SonarCloud token in Jenkins credentials
    }
    stages {
        stage('Checkout Code') {
            steps {
                git branch: 'master', url: 'https://github.com/drm0011/BookClubApp.git'
            }
        }

        stage('Setup .NET') {
            steps {
                sh 'dotnet --version || curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0'
            }
        }

        stage('Build Project') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Run Unit Tests') {
            steps {
                sh 'dotnet test --configuration Release'
            }
        }

        stage('SonarCloud Analysis') {
            steps {
                sh '''
                dotnet tool install --global dotnet-sonarscanner
                dotnet tool install --global coverlet.console
                export PATH="$PATH:/var/jenkins_home/.dotnet/tools"
                dotnet sonarscanner begin /k:"drm0011_BookClubApp" /o:"drm0011" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="${SONAR_TOKEN}" /d:sonar.cs.opencover.reportsPaths="**/TestResults/*/coverage.opencover.xml"
                dotnet build
                dotnet test --collect:"XPlat Code Coverage" /p:CoverletOutputFormat=opencover /p:CoverletOutput=./TestResults/
                dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
                '''
            }
        }

        stage('Docker Build and Push') {
            steps {
                sh '''
                echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
                docker build -t ${DOCKER_USERNAME}/book-club-app-image .
                docker push ${DOCKER_USERNAME}/book-club-app-image
                '''
            }
        }
    }
}
