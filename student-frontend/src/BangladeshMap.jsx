import { useEffect, useState } from 'react';
import { MapContainer, GeoJSON } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';

function BangladeshMap() {
  const [geoData, setGeoData] = useState(null);
  const [error, setError] = useState(false);

  const studentData = {
    "Dhaka": 45,
    "Chittagong": 30,
    "Chattogram": 30,
    "Rajshahi": 15,
    "Khulna": 20,
    "Sylhet": 12,
    "Barisal": 8,
    "Rangpur": 10,
    "Mymensingh": 3
  };

  useEffect(() => {
    // নতুন এবং ১০০% রিলায়েবল একটি গিটহাব লিংক
    fetch('https://raw.githubusercontent.com/ifahimreza/bangladesh-geojson/master/src/data/bangladesh.geojson')
      .then(res => {
        if (!res.ok) throw new Error("Network error");
        return res.json();
      })
      .then(data => setGeoData(data))
      .catch(err => {
        console.error("Map data loading error: ", err);
        setError(true);
      });
  }, []);

  const defaultStyle = {
    fillColor: '#1e223c',
    weight: 1,
    opacity: 1,
    color: 'white',
    fillOpacity: 0.8
  };

  const onEachDivision = (feature, layer) => {
    // বিভিন্ন সোর্সে নামের প্রোপার্টি ভিন্ন হতে পারে, তাই সব অপশন রাখা হলো
    const divisionName = feature.properties.NAME_1 || feature.properties.name || feature.properties.ADM1_EN || feature.properties.Division; 
    
    let students = 0;
    Object.keys(studentData).forEach(key => {
      if (divisionName && divisionName.includes(key)) {
        students = studentData[key];
      }
    });

    layer.on({
      mouseover: (e) => {
        const target = e.target;
        target.setStyle({
          fillColor: '#267edc', // হোভার করলে লাল রঙ হবে
          fillOpacity: 1,
          weight: 2
        });
        target.bringToFront();
      },
      mouseout: (e) => {
        const target = e.target;
        target.setStyle(defaultStyle);
      }
    });

    layer.bindTooltip(`
      <div style="text-align:center;">
        <strong style="font-size:14px; color:#1e223c;">${divisionName || 'Unknown'} Division</strong><br/>
        <span style="color:#555;">Students: ${students}</span>
      </div>
    `, { sticky: true });
  };

  return (
<div className="map-wrapper" style={{ background: '#f8f9fc', height: '550px', width: '100%', borderRadius: '8px', overflow: 'hidden' }}>
      {!geoData && !error && (
        <div style={{ height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', color: '#555' }}>
          Loading Map Data...
        </div>
      )}
      
      {error && (
        <div style={{ height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', color: 'red' }}>
          Failed to load map data. Please try refreshing.
        </div>
      )}
      
      {geoData && (
        <MapContainer 
        center={[23.6850, 90.3563]} 
        zoom={6} 
        scrollWheelZoom={false} 
        style={{ height: '100%', width: '100%', background: 'transparent' }}
        >
          <GeoJSON 
            key="bd-map-new"
            data={geoData} 
            style={defaultStyle} 
            onEachFeature={onEachDivision} 
          />
        </MapContainer>
      )}
    </div>
  );
}

export default BangladeshMap;