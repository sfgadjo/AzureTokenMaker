﻿using System;
using System.Collections.Generic;
using System.Linq;
using AzureTokenMaker.App.Properties;
using Newtonsoft.Json;
using AzureTokenMaker.App.Model;

namespace AzureTokenMaker.App.Services {
    sealed class ProfileService {

        private readonly HashSet<Profile> _profiles;

        private ProfileService ( HashSet<Profile> profiles ) {
            _profiles = profiles;
        }

        public void Save(Profile profile) {
            if (profile == null) {
                throw new ArgumentNullException("profile");
            }

            _profiles.RemoveWhere(p => p.Equals(profile));
            _profiles.Add(profile);
            internalSave();
        }

        public void Delete(Profile profile) {
            if (profile == null) {
                throw new ArgumentNullException("profile");
            }

            _profiles.RemoveWhere( p => p.Equals( profile ) );
            internalSave();
        }

        public ISet<Profile> GetProfiles () {
            var result = deserializeProfiles();
            
            return result;
        }

        public static ProfileService Init() {
            var profiles = deserializeProfiles();
            var result = new ProfileService( profiles );
            return result;

        }

        void internalSave() {
            var raw = serializeProfiles( _profiles );
            Settings.Default.Profiles = raw;
            Settings.Default.Save();
        }

        static string serializeProfiles ( IEnumerable<Profile> profiles ) {
            string raw = JsonConvert.SerializeObject(profiles.ToList());
            return raw;
        }

        static HashSet<Profile> deserializeProfiles () {
            string raw = Settings.Default.Profiles;
            if (String.IsNullOrWhiteSpace(raw)) {
                return new HashSet<Profile>();
            }
            var result = JsonConvert.DeserializeObject<List<Profile>>( raw );
            result = result.OrderBy(s =>s.Name).ToList();
            return new HashSet<Profile>(result);
        }
    }
}
