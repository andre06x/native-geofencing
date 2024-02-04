import React, { useEffect, useState } from "react";
import { Button, Text, View, ToastAndroid, Alert } from "react-native";
import * as Location from "expo-location";
import * as TaskManager from "expo-task-manager";

const GEOFENCING_TASK_NAME = "GEOFENCING_TASK_8792";

//const LOCATION_TASK_NAME = "LOCATION_UPDATES_TASK";

// TaskManager.defineTask(LOCATION_TASK_NAME, ({ data, error }) => {
//   if (error) {
//     console.error("Location task error:", error);
//     return;
//   }
//   console.log("Received new location:", data);
// });

TaskManager.defineTask(GEOFENCING_TASK_NAME, ({ data: { eventType, region }, error }) => {
  if (error) {
    console.error("Error:", error);
    return;
  }
  console.log(eventType);
  switch (eventType) {
    case Location.GeofencingEventType.Enter:
      Alert.alert("Entered Title", "Entered", [
        {
          text: "Cancel",
          onPress: () => console.log("Cancel Pressed"),
          style: "cancel",
        },
        { text: "OK", onPress: () => console.log("OK Pressed") },
      ]);
      console.log("Entered geofence1:", region);

      break;
    case Location.GeofencingEventType.Exit:
      Alert.alert("Exited Title", "Exited", [
        {
          text: "Cancel",
          onPress: () => console.log("Cancel Pressed"),
          style: "cancel",
        },
        { text: "OK", onPress: () => console.log("OK Pressed") },
      ]);
      ToastAndroid.show("Exited geofence", ToastAndroid.SHORT);

      break;
    default:
      console.log("Unknown geofence event type");
  }
});

export default function App() {
  const [geofenceStatus, setGeofenceStatus] = useState("");
  const [number, setNumber] = useState(0);

  //const [status, requestPermission] = Location.useBackgroundPermissions();

  useEffect(() => {
    (async () => {
      const a = await Location.isBackgroundLocationAvailableAsync();
      let { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== "granted") {
        console.error("Permission to access location was denied");
        return;
      }

      let { status: backgroundStatus } =
        await Location.requestBackgroundPermissionsAsync();
      if (backgroundStatus !== "granted") {
        console.error("Permission to access location in background was denied");
        return;
      }

      let location = await Location.getCurrentPositionAsync({});
      console.log("Current location:", location);

      //seta padrao a propria lat lng para geofencing
      const region = {
        latitude: location.coords.latitude,
        longitude: location.coords.longitude,
        radius: 100, // metros
        notifyOnEnter: true,
        notifyOnExit: true,
      };

      console.log("Geofencing", region);

      Location.startGeofencingAsync(GEOFENCING_TASK_NAME, [region])
        .then(() => console.log("Geofencing started"))
        .catch((error) => console.error("Failed to start geofencing:", error));

      //Location.startLocationUpdatesAsync("LOCATION_UPDATES_TASK", {
      //  accuracy: Location.Accuracy.Balanced,
      //  timeInterval: 1000,
      //  distanceInterval: 0,
      //});

      // const intervalId = setInterval(async () => {
      //   let location = await Location.getCurrentPositionAsync({});
      //   console.log("Current location:", location);
      // }, 1000);
    })();
  }, []);

  return (
    <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
      <Text>Geofencing App</Text>
      <Text>Status: {geofenceStatus}</Text>
      <Button
        title={number.toLocaleString()}
        onPress={() => setNumber((number) => number + 1)}
      />
    </View>
  );
}
